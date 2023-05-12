using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyParkingApp.Models
{
	[Table("Clients")]
	public class Client
	{
		[Required]
		[Column("Id")]
		public int Id { get; set; }

		[Required]
		[Column("Plate")]
		public string Plate { get; set; }

		[Required]
		[Column("AdmissionDateTime")]
		public DateTime AdmissionDateTime { get; set; }

		[Required]
		[Column("ElectricHybrid")]
		public bool ElectricHybrid { get; set; }

		[Required]
		[Column("PlaceId")]
		public int PlaceId { get; set; }

		[Column("DepatureDateTime")]
		public DateTime DepatureDateTime { get; set; }

		[Column("Discount")]
		public Decimal Discount { get; set; }

		[Required]
		[Column("VehicleTypeId")]
		public int VehicleTypeId { get; set; }

        [Column("TotalPay")]
        public int TotalPay { get; set; }

        [Required]
        [Column("State")]
        public bool State { get; set; }
    }
}

