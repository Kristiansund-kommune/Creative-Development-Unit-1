using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Concepts.Entities;

public class UserStats
{

	[ForeignKey("OwnerId")]
	public User? Owner { get; set; }
	public int OwnerId { get; set; }
	[Key]
	public int Id { get; set; }
	[MaxLength(255)]
	[ForeignKey("LevelId")]
	public UserLevel? Level { get; set; }
	public int LevelId { get; set; }
	public int NumberOfRides { get; set; }
	public double TotalDistance { get; set; }
	public int TotalTime { get; set; }
	public double EvaluationTotal { get; set; }

	public long CurrentPoints { get; set; }
	[NotMapped]
	public bool IsNewLevel { get; set; }
}
