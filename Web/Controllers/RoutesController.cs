using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Core.Concepts.Repository;
using Route = Core.Concepts.Entities.Route;
using Microsoft.AspNetCore.Http;
namespace Web.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class RoutesController : ControllerBase
{

    private readonly IRidesDbRepository _ridesRepository;

    /// <summary>
    /// Controller for handling routes.
    /// </summary>
    public RoutesController(IRidesDbRepository ridesRepository)
    {
        _ridesRepository = ridesRepository;
    }

    /// <summary>
    /// Retrieves the routes associated with a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>A list of routes associated with the user.</returns>
    [HttpGet(Name = "GetUsersRides")]
    public async Task<List<Route>> GetUserById(int id)
    {
        return await _ridesRepository.GetUserRoutes(id);
    }

}
