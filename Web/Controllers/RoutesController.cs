using Core.Concepts.Repository;
using Route = Core.Concepts.Entities.Route;

namespace Web.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public class RoutesController : ControllerBase
{

	private readonly IRidesDbRepository _ridesRepository;

	public RoutesController(IRidesDbRepository ridesRepository)
	{
		_ridesRepository = ridesRepository;
	}


	[HttpGet(Name = "GetUsersRides")]
	public async Task<List<Route>> GetUserById(int id)
	{
		return await _ridesRepository.GetUserRoutes(id);
	}

}
