using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concepts.Entities
{
    public class Route
    {
        [Key]
        public int RouteId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [Column(TypeName ="decimal(9,6)")]
        public double StartPointLon { get; set; }

        [Required]
        [Column(TypeName ="decimal(9,6)")]
        public double StartPointLat { get; set; }

        [Required]
        [Column(TypeName ="decimal(9,6)")]
        public double EndPointLon { get; set; }

        [Required]
        [Column(TypeName ="decimal(9,6)")]
        public double EndPointLat { get; set; }

        // Additional related information about the travel
        public float? ActivityDistance { get; set; }
	    public TimeSpan? Duration { get; set; }

    }


}
