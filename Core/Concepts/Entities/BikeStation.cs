using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Concepts.Entities
{

    public class BikeStation
    {
        [Key]
        public int StationId { get; set; }

        [Required, StringLength(100)]
        public string StationName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName ="decimal(9,6)")]
        public double Lat { get; set; }

        [Required]
        [Column(TypeName ="decimal(9,6)")]
        public double Lon { get; set; }
		public List<BikeStationDock> BikeStationDocks { get; set; }

        [NotMapped]
        public int AvailableDocks { get; set; }
        [NotMapped]
        public int TotalDocks { get; set; }
    }

}
