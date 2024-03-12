using System.Data.Entity.Core.Mapping;
using System.Text;
using System.Text.Json;
using Core.Concepts.Entities;
using Core.Concepts.Repository;
using Newtonsoft.Json.Linq;

namespace Web.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{

	private readonly IUserRepository _userRepository;
	private readonly IUserStatsRepository _userStatsRepository;
	private readonly IConfiguration _configuration;
	private readonly string _redirectURI = "http://localhost:5291/User/ProcessExchangeToken/exchange_token";
	private readonly string StravaOAuthURL;
	private readonly string _stravaClientID;
	public UserController(IUserRepository userRepository,
		IUserStatsRepository userStatsRepository, IConfiguration configuration)
	{
		_userRepository = userRepository;
		_userStatsRepository = userStatsRepository;
		_configuration = configuration;

		var _stravaClientID= _configuration["Strava:ClientId"];
		if (_stravaClientID == null)
		{
			throw new ArgumentNullException("Strava client id cannot be null");
		}

		StravaOAuthURL = $"https://www.strava.com/oauth/authorize?client_id={_stravaClientID}&redirect_uri={_redirectURI}&response_type=code&approval_prompt=force&scope=read,activity:read_all";

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
			var stats = await _userStatsRepository.FetchAndUpdateStravaStats(user);
			user.Stats = stats;
		}

		return user;
	}

	[HttpGet(Name = "GetUsersToStrava")]
	public IActionResult RedirectUserToStrava()
	{
		return Redirect(StravaOAuthURL);
	}


    [HttpGet("exchange_token")]
    public async Task<IActionResult> ProcessExchangeToken(string code)
    {
	    var clientId = _configuration["Strava:ClientId"];
	    var clientSecret = _configuration["Strava:Client_Secret"];

	    if (clientId == null || clientSecret == null)
	    {
		    return StatusCode(500);
	    }

	    await this.RegisterUserWithStrava( clientId, clientSecret, code);
	    return Ok();
    }

    [HttpPost("RegisterUserWithStrava")]
    public async Task<IActionResult> RegisterUserWithStrava(string clientId, string clientSecret, string authorizationCode)
    {
	    using (var httpClient = new HttpClient())
	    {
		    var request = new HttpRequestMessage(HttpMethod.Post, "https://www.strava.com/oauth/token");

		    request.Content = new StringContent(JsonSerializer.Serialize(new
		    {
			    client_id = clientId,
			    client_secret = clientSecret,
			    code = authorizationCode,
			    grant_type = "authorization_code"
		    }), Encoding.UTF8, "application/json");

		    var response = await httpClient.SendAsync(request);


 		    if (response.IsSuccessStatusCode)
	        {


			    string responseBody = await response.Content.ReadAsStringAsync();
			    var result = ParseJsonResponse(responseBody);

			    var foundUser = await _userRepository.GetUserByStravaID(result.id);
			    if (foundUser == null)
			    {
				    User newUser = new User
				    {
					    Name = result.name,
					    StravaId = result.id,
					    Email = "No email",
					    RegistrationDate = DateTime.Today,
					    AccessToken = result.accessToken,
					    RefreshToken = result.refreshToken,
					    ExpiresAt = result.expires_at


				    };
				   foundUser = await _userRepository.AddUser(newUser);
			    }

			    if (foundUser == null)
			    {
				    return StatusCode(500);
			    }

			    foundUser.Stats = await _userStatsRepository.FetchAndUpdateStravaStats(foundUser);

			    return Ok(foundUser);
		    }

		    return StatusCode(500);
	    }
    }
    private (string name, long id, string accessToken, string refreshToken, DateTime expires_at)
	    ParseJsonResponse(string jsonResponse)
    {
	    try
	    {
		    JObject responseObj = JObject.Parse(jsonResponse);
		    string firstName = (string)responseObj.SelectToken("athlete.firstname");
		    string lastName = (string)responseObj.SelectToken("athlete.lastname");
		    string accessToken = (string)responseObj.SelectToken("access_token");
		    string refreshToken = (string)responseObj.SelectToken("refresh_token");
		    long expiresAtNum = (long)responseObj.SelectToken("expires_at");

		    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expiresAtNum);
		    DateTime expiresAt = dateTimeOffset.UtcDateTime;


		    int id = (int)responseObj.SelectToken("athlete.id");

		    return (firstName + " " + lastName, id, accessToken, refreshToken, expiresAt);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"Error while parsing the JSON: {e}");
		    throw;
	    }
    }
}
