using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyParkingApp.Models.Dto
{
	public class ClientDto
	{
        public int Id { get; set; }

        public string Plate { get; set; }

        public string? AdmissionDateTime { get; set; }

        public bool ElectricHybrid { get; set; }

        public int PlaceId { get; set; }

        public string DepatureDateTime { get; set; }

        public bool Discount { get; set; }

        public int VehicleTypeId { get; set; }

        public string? TotalPay { get; set; }

        public bool State { get; set; }
    }
}

