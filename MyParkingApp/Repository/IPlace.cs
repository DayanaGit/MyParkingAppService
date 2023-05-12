using System;
using MyParkingApp.Models;

namespace MyParkingApp.Repository
{
	public interface IPlace
	{
        Task<IEnumerable<Place>> GetPlaces();
        Task<Place> GetPlace(int id);
    }
}

