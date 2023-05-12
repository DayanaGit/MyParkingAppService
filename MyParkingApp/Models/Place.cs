using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyParkingApp.Models
{
    [Table("Places")]
    public class Place
	{
        [Required]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [Column("VehicleTypeId")]
        public int VehicleTypeId { get; set; }

        [Required]
        [Column("Available")]
        public bool Available { get; set; }
    }
}

