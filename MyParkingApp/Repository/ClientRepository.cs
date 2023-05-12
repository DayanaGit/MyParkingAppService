using System;
using MyParkingApp.Models;
using Dapper;
using MyParkingApp.Models.Dto;
using AutoMapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MyParkingApp.Repository
{
	public class ClientRepository: IClient
	{
        public readonly MySqlConnection _db;

		public ClientRepository(MySqlConnection db)
		{
            _db = db;
		}

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_db.ConnectionString);
        }

        public async Task<bool> CreateClient(ClientDto clientDto)
        {
            var db = dbConnection();
            var discountNumber = 0.0;
            db.Open();
            try
            {
                clientDto.AdmissionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var ing = DateTime.Parse(clientDto.AdmissionDateTime);
                var outs = DateTime.Parse(clientDto.DepatureDateTime);
                TimeSpan hours = ing.Subtract(outs);
                if (clientDto.VehicleTypeId == 1)
                {
                    clientDto.TotalPay = (62 * Convert.ToInt32(hours.Minutes)).ToString();
                }
                else
                {
                    clientDto.TotalPay = (120 * Convert.ToInt32(hours.Hours)).ToString();
                }
                if (clientDto.Discount == true)
                {
                    var total = Convert.ToDouble(clientDto.TotalPay);
                    discountNumber = 0.25;
                    clientDto.TotalPay = (total - (total * discountNumber)).ToString();
                }
                var sql = @"INSERT INTO Clients (
                        Plate,
                        AdmissionDateTime,
                        PlaceId,
                        DepatureDateTime,
                        Discount,
                        VehicleTypeId,
                        ElectricHybrid,
                        State,
                        TotalPay
                        )VALUES(
                        @Plate,
                        @AdmissionDateTime,
                        @PlaceId,
                        @DepatureDateTime,
                        @discountNumber,
                        @VehicleTypeId,
                        @ElectricHybrid,
                        @State,
                        @TotalPay
                        )";
                var res = await db.ExecuteAsync(sql, new
                {
                    clientDto.Plate,
                    clientDto.AdmissionDateTime,
                    clientDto.PlaceId,
                    clientDto.DepatureDateTime,
                    discountNumber,
                    clientDto.VehicleTypeId,
                    clientDto.ElectricHybrid,
                    clientDto.State,
                    clientDto.TotalPay
                });
                var sqlUpdate = @"UPDATE Places SET Available=0 WHERE Id=@Id";
                var resUpdate = await db.ExecuteAsync(sqlUpdate, new { Id = clientDto.PlaceId });
                db.Close();
                return res > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            

        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                var db = dbConnection();
                db.Open();
                var sql = @"DELETE FROM Clients WHERE Id = @id";
                var res = await db.ExecuteAsync(sql, new { Id = id });
                db.Close();
                return res > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<Client> GetClient(int id)
        {
            var res = new Client();
            try
            {
                var db = dbConnection();
                db.Open();
                var sql = @"SELECT
                        Id,
                        Plate,
                        AdmissionDateTime,
                        PlaceId,
                        DepatureDateTime,
                        Discount,
                        VehicleTypeId,
                        ElectricHybrid,
                        TotalPay,
                        State
                        FROM Clients WHERE Id = @Id";
                 res = await db.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
                db.Close();
                return res;
            }
            catch (Exception ex)
            {
                return res ;
            }
            
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            IEnumerable<Client>? res;
            try
            {
                var db = dbConnection();
                db.Open();
                var sql = @"SELECT
                            Id,
                            Plate,
                            AdmissionDateTime,
                            PlaceId,
                            DepatureDateTime,
                            Discount,
                            VehicleTypeId,
                            ElectricHybrid,
                            TotalPay,
                            State
                            FROM Clients";
                res = await db.QueryAsync<Client>(sql);
                db.Close();
                return res;
            }
            catch (Exception ex)
            {
                return res = null;
            }
        }

        public async Task<bool> UpdateClient(ClientDto clientDto)
        {
            try
            {
            var db = dbConnection();
            Client clientExist = await GetClient(clientDto.Id);
            var discountNumber = 0.0;

                var ing = DateTime.Parse(clientDto.AdmissionDateTime);
                var outs = DateTime.Parse(clientDto.DepatureDateTime);
                TimeSpan hours = outs.Subtract(ing);
                if (clientDto.VehicleTypeId == 1)
                {
                    clientDto.TotalPay = (62 * Convert.ToInt32(hours.Hours)).ToString();
                }
                else
                {
                    clientDto.TotalPay = (120 * Convert.ToInt32(hours.Hours)).ToString();
                }
                if (clientDto.Discount == true)
                {
                    var total = Convert.ToDouble(clientDto.TotalPay);
                    discountNumber = 0.25;
                    clientDto.TotalPay = (total - (total * discountNumber)).ToString();
                }
                db.Open();

            var sql = @"UPDATE Clients SET
                        Plate = @Plate,
                        AdmissionDateTime = @AdmissionDateTime,
                        PlaceId = @PlaceId,
                        DepatureDateTime = @DepatureDateTime,
                        Discount = @discountNumber,
                        VehicleTypeId = @VehicleTypeId,
                        ElectricHybrid = @ElectricHybrid,
                        TotalPay = @TotalPay,
                        State = @State
                        WHERE Id = @Id";
            var res = await db.ExecuteAsync(sql, new
            {
                clientDto.Plate,
                clientDto.AdmissionDateTime,
                clientDto.DepatureDateTime,
                clientDto.PlaceId,
                discountNumber,
                clientDto.VehicleTypeId,
                clientDto.ElectricHybrid,
                clientDto.TotalPay,
                clientDto.State,
                Id = clientDto.Id
            });
            if (clientDto.PlaceId != clientExist.PlaceId)
            {
                var sqlUpdateNew = @"UPDATE Places SET Available=0 WHERE Id=@Id";
                var resUpdateNew = await db.ExecuteAsync(sqlUpdateNew, new { Id = clientDto.PlaceId });
                var sqlUpdateOld = @"UPDATE Places SET Available=1 WHERE Id=@Id";
                var resUpdateOld = await db.ExecuteAsync(sqlUpdateOld, new { Id = clientExist.PlaceId });
            }
            db.Close();
            return res > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

