using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concepts.Entities
{
    public enum BikeType
    {
        Mountain,
        Road,
        Hybrid
    }


    public class Bike
    {
        [Key]
        public int BikeId { get; set; }

        [ForeignKey("OwnerId")]
        public User? Owner { get; set; }
        public int OwnerId { get; set; }
        public BikeType BikeType { get; set; }
    }
}
