using Core.Concepts.Entities;
using Core.Concepts.Repository;

namespace Web.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BikeStationsController : ControllerBase
{

	private readonly IBikeStationRepository _bikeStationRepository;

	public BikeStationsController(IBikeStationRepository bikeStationRepository)
	{
		_bikeStationRepository = bikeStationRepository;
	}

	/// <summary>
	/// Retrieves all the bike stations.
	/// </summary>
	/// <returns>An IEnumerable of BikeStation objects representing all the bike stations.</returns>
	[HttpGet(Name = "GetBikeStations")]
	public async Task<IEnumerable<BikeStation>> Get()
	{
		var stations = (await _bikeStationRepository.GetStations()).ToList();
		foreach (var station in stations)
		{
			station.TotalDocks = station.BikeStationDocks.Count;
			station.AvailableDocks = station.BikeStationDocks.Count(e => e.Status == StationDockStatus.Available);
		}

		return stations;
	}


}
