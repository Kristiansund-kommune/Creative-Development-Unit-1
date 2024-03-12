using System.ComponentModel.DataAnnotations;

namespace Core.Concepts.Entities;

public class UserLevel
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }
	[Required]
	public long RequiredPoints { get; set; }

}
