using Core.Concepts.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Core.Concepts.Repository
{
	public interface IUserRepository : IDisposable
	{
		Task<User?> GetUserById(int id);
		Task<User?> GetUserByEmail(string email);
		Task<User?> AddUser(User newUser);
		Task<User?> GetUserByStravaID(long id);
		Task<User?> UpdateToken(User user);
	}

	public class UserRepository : IUserRepository, IDisposable
	{
		private bool disposed = false;
		private readonly StationsContext _context;

		private readonly IConfiguration _configuration;
		public UserRepository(StationsContext context,  IConfiguration configuration)
		{
			_context = context;

			_configuration = configuration;
		}

		public async Task<User?> GetUserById(int id)
		{
			return await _context.Users
				.Include(e => e.Bikes)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			return await _context.Users
				.Include(e => e.Bikes)
				.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<User?> GetUserByStravaID(long id)
		{
			return await _context.Users
				.Include(e => e.Bikes)
				.FirstOrDefaultAsync(x => x.StravaId == id);
		}

		public async Task<User?> UpdateToken(User user)
		{
			if (user.RefreshToken == null)
			{
				throw new AggregateException();
			}

			var request = new HttpRequestMessage(HttpMethod.Post, "https://www.strava.com/api/v3/oauth/token");


			request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{ "client_id", _configuration["Strava:ClientId"] },
				{ "client_secret", _configuration["Strava:Client_Secret"] },
				{ "grant_type", "refresh_token" },
				{ "refresh_token", user.RefreshToken }
			});
			try
			{
				using (var httpClient = new HttpClient())
				{
					var response = await httpClient.SendAsync(request);

					response.EnsureSuccessStatusCode();

					if (response.IsSuccessStatusCode)
					{
						var stringResponse = await response.Content.ReadAsStringAsync();
						var result = ParseJsonResponse(stringResponse);
						user.RefreshToken = result.refreshToken;
						user.AccessToken = result.accessToken;
						user.ExpiresAt = result.expires_at;
						_context.Users.Update(user);

						await _context.SaveChangesAsync();

						return user;
					}

				}
				throw new Exception();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task<User?> AddUser(User newUser)
		{
			try
			{
				var entry = await _context.Users.AddAsync(newUser);
				await _context.SaveChangesAsync();

				return entry.Entity;
			}
			catch
			{
				return null;
			}
		}

		private (string accessToken, string refreshToken, DateTime expires_at)
			ParseJsonResponse(string jsonResponse)
		{
			try
			{
				JObject responseObj = JObject.Parse(jsonResponse);
				string accessToken = (string)responseObj.SelectToken("access_token");
				string refreshToken = (string)responseObj.SelectToken("refresh_token");
				long expiresAtNum = (long)responseObj.SelectToken("expires_at");

				DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expiresAtNum);
				DateTime expiresAt = dateTimeOffset.UtcDateTime;

				return (accessToken, refreshToken, expiresAt);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error while parsing the JSON: {e}");
				throw;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
