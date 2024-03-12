using System.Data.Entity.Core.Mapping;
using Core.Concepts.Entities;
using Core.Concepts.Repository;

namespace Web.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{

	private readonly IUserRepository _userRepository;
	private readonly IUserStatsRepository _userStatsRepository;

	public UserController(IUserRepository userRepository,
		IUserStatsRepository userStatsRepository)
	{
		_userRepository = userRepository;
		_userStatsRepository = userStatsRepository;
	}


	[HttpGet(Name = "GetUserById")]
	public async Task<User?> GetUserById(int id)
	{
		var user = await _userRepository.GetUserById(id);
		return user;
	}

	[HttpGet(Name = "GetUserByEmail")]
	public async Task<User?> GetUserByEmail(string email)
	{
		var user = await _userRepository.GetUserByEmail(email);
		return user;
	}

	[HttpGet(Name = "GetUserWithStats")]
	public async Task<User?> GetUserWithStats(int id)
	{
		var user = await _userRepository.GetUserById(id);
		if (user != null)
		{
			var stats = await _userStatsRepository.UpdateUserStats(user.Id);
			user.Stats = stats;
		}

		return user;
	}


}
