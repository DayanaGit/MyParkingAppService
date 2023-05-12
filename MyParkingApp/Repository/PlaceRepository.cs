using System;
using Dapper;
using MyParkingApp.Models;
using MySql.Data.MySqlClient;

namespace MyParkingApp.Repository
{
	public class PlaceRepository: IPlace
	{
        public readonly MySqlConnection _db;

        public PlaceRepository(MySqlConnection db)
        {
            _db = db;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_db.ConnectionString);
        }

        public async Task<Place> GetPlace(int id)
        {
            var db = dbConnection();
            db.Open();
            var sql = @"SELECT
                        Id,
                        Name,
                        VehicleTypeId,
                        Available
                        FROM Places WHERE Id = @Id";
            Place res = await db.QueryFirstOrDefaultAsync<Place>(sql, new { Id = id });
            db.Close();
            return res;
        }

        public async Task<IEnumerable<Place>> GetPlaces()
        {
            var db = dbConnection();
            db.Open();
            var sql = @"SELECT
                        Id,
                        Name,
                        VehicleTypeId,
                        Available
                        FROM Places WHERE Available = 1";
            var res = await db.QueryAsync<Place>(sql);
            db.Close();
            return res;
        }
    }
}

