using System;
namespace MyParkingApp.Models.Dto
{
	public class PlaceDto
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public int VehicleTypeId { get; set; }

        public bool Available { get; set; }
    }
}

