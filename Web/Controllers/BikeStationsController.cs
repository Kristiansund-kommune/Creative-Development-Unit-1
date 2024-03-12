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
	public IEnumerable<BikeStation> Get()
	{
		return _bikeStationRepository.GetStations();
	}


}
