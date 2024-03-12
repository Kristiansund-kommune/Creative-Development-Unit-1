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
