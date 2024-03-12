using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concepts.Entities
{

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public long CurrentPoints { get; set; }

		[NotMapped]
        public List<Bike> Bikes { get; set; }

        [NotMapped]
        public string Level { get; set; }

    }
}
