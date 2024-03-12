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

	public UserController(IUserRepository userRepository)
	{
		_userRepository = userRepository;
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


}
