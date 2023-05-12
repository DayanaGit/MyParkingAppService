using System;
using MyParkingApp.Models;
using Dapper;
using MyParkingApp.Models.Dto;
using AutoMapper;
using MySql.Data.MySqlClient;

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

                if (clientDto.Discount == true)
                {
                    discountNumber = 0.25;
                }
                var sql = @"INSERT INTO Clients (
                        Plate,
                        AdmissionDateTime,
                        PlaceId,
                        DepatureDateTime,
                        Discount,
                        VehicleTypeId,
                        ElectricHybrid,
                        State
                        )VALUES(
                        @Plate,
                        @AdmissionDateTime,
                        @PlaceId,
                        @DepatureDateTime,
                        @discountNumber,
                        @VehicleTypeId,
                        @ElectricHybrid,
                        @State
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
                    clientDto.State
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
            var db = dbConnection();
            db.Open();
            var sql = @"DELETE FROM Clients WHERE Id = @id";
            var res = await db.ExecuteAsync(sql, new { Id = id });
            db.Close();
            return res > 0;
        }

        public async Task<Client> GetClient(int id)
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
            Client res = await db.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
            db.Close();
            return res;
        }

        public async Task<IEnumerable<Client>> GetClients()
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
            var res = await db.QueryAsync<Client>(sql);
            db.Close();
            return res;
        }

        public async Task<bool> UpdateClient(ClientDto clientDto)
        {
            var db = dbConnection();
            Client clientExist = await GetClient(clientDto.Id);
            var discountNumber = 0.0;

            if (clientDto.Discount == true)
            {
                discountNumber = 0.25;
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
                clientDto.State
            });
            if (clientDto.PlaceId != clientExist.PlaceId)
            {
                var sqlUpdateNew = @"UPDATE Places SET Available=0 WHERE Id=@placeIdNew";
                var resUpdateNew = await db.ExecuteAsync(sqlUpdateNew, new { Id = clientDto.PlaceId });
                var sqlUpdateOld = @"UPDATE Places SET Available=1 WHERE Id=@placeIdOld";
                var resUpdateOld = await db.ExecuteAsync(sqlUpdateOld, new { Id = clientExist.PlaceId });
            }
            db.Close();
            return res > 0;
        }
    }
}

