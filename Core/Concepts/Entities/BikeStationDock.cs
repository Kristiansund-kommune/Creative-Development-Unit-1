using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concepts.Entities;

public enum StationDockStatus
{
	Available = 1,
	Used = 2,
	Maintenance  = 3
}

public class BikeStationDock
{
	[Key]
	public int Id { get; set; }
	[Required]
	public int BikeStationId { get; set; }
	[ForeignKey("BikeStationId")]
	public BikeStation? BikeStation { get; set; }
	[Required]
	public StationDockStatus Status { get; set; }
}
