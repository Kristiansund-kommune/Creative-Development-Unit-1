using System.Text;
using Core.Concepts.Entities;
using Core.Concepts.Repository;
using Microsoft.Azure.Devices;
namespace Web.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public class BikeStationsController : ControllerBase
{
	private readonly IConfiguration _configuration;
	private readonly IBikeStationRepository _bikeStationRepository;

	public BikeStationsController(IBikeStationRepository bikeStationRepository, IConfiguration configuration)
	{
		_bikeStationRepository = bikeStationRepository;
		_configuration = configuration;
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

	[HttpPost(Name = "LockStand")]
	public async Task LockStand(string deviceName, int lockId)
	{
		var serviceClient = ServiceClient.CreateFromConnectionString(_configuration["IotHub:ConnectionString"]);
		var commandMessage = new Message(Encoding.ASCII.GetBytes($"lock:{lockId}"));
		await serviceClient.SendAsync(deviceName, commandMessage);
	}

	[HttpPost(Name = "UnlockStand")]
	public async Task UnlockStand(string deviceName, int lockId)
	{
		var serviceClient = ServiceClient.CreateFromConnectionString(_configuration["IotHub:ConnectionString"]);
		var commandMessage = new Message(Encoding.ASCII.GetBytes($"unlock:{lockId}"));
		await serviceClient.SendAsync(deviceName, commandMessage);
	}
}
