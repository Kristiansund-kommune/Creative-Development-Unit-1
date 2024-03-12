using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Core.Concepts.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Core.Concepts.Repository;

public interface IUserStatsRepository: IDisposable
{
	public Task<UserStats> UpdateUserStats(int userId);
	public Task<UserStats?> FetchAndUpdateStravaStats(User user);


}

public class UserStatsRepository : IUserStatsRepository
{
	private bool disposed = false;
	private readonly StationsContext _context;
	private readonly IRidesDbRepository _ridesDbRepository;
	private readonly IUserRepository _userRepository;
	private readonly IConfiguration _configuration;

	public UserStatsRepository(StationsContext context, IRidesDbRepository ridesDbRepository, IUserRepository userRepository,
		IConfiguration configuration)
	{
		_context = context;
		_ridesDbRepository = ridesDbRepository;
		_userRepository = userRepository;
		_configuration = configuration;
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


	public async Task<UserStats> UpdateUserStats(int userId)
	{
		try
		{
			var user = await _userRepository.GetUserById(userId);
			if (user == null)
			{
				throw new ArgumentNullException();
			}

			var userRides = await _ridesDbRepository.GetUserRoutes(userId);
			UserStats stats = new UserStats();
			foreach (var ride in userRides)
			{
			//	stats.CurrentPoints = Convert.ToInt64(ExperienceCalculation.CalculateTotalEXP(50	, 15, (double) ride.ActivityDistance, 15.3));
			}

			stats.NumberOfRides = userRides.Count;
			stats.Level = GetLevel(stats.CurrentPoints);

			var foundStat = await _context.UserStats.Include(userStats => userStats.Level).FirstOrDefaultAsync(e => e.Owner.Id == user.Id);

			if (foundStat == null)
			{
				stats.Owner = user;
				stats.IsNewLevel = true;
				_context.UserStats.Add(stats);
			}
			else
			{
				foundStat.Owner = user;
				foundStat.TotalDistance = stats.TotalDistance;
				foundStat.TotalTime = stats.TotalTime;
				foundStat.CurrentPoints = stats.CurrentPoints;
				if (foundStat.Level.Title != stats.Level.Title)
				{
					foundStat.IsNewLevel = true;
				}
				foundStat.Level = stats.Level;
				foundStat.IsNewLevel = stats.IsNewLevel;
				_context.UserStats.Update(foundStat);
				stats = foundStat;
			}
			await _context.SaveChangesAsync();

			return stats;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

	}

	/// <summary>
	/// Fetches and updates the Strava statistics for a given user.
	/// </summary>
	/// <param name="user">The user for whom to fetch and update the Strava statistics.</param>
	/// <returns>The updated UserStats object or null if fetching the statistics failed.</returns>
	public async Task<UserStats?> FetchAndUpdateStravaStats(User user)
	{
		try
		{
			if (user.StravaId == null)
			{
				throw new ArgumentException("Strava Id cannot be null");
			}

			var now = DateTime.Now;
			if (user.ExpiresAt == null || user.ExpiresAt < now)
			{
				user = await _userRepository.UpdateToken(user);
			}

			if (user == null)
			{
				throw new AggregateException();
			}

		using (var httpClient = new HttpClient())
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.strava.com/api/v3/athletes/{user.StravaId}/stats");

			// Set the 'Authorization' header to the bearer token
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

				var response = await httpClient.SendAsync(request);


				if (response.IsSuccessStatusCode)
				{

					string responseBody = await response.Content.ReadAsStringAsync();
					var stats = ParseJsonResponse(responseBody);
					stats.CurrentPoints = Convert.ToInt64(ExperienceCalculation.CalculateTotalEXP(stats.EvaluationTotal, stats.TotalDistance, stats.TotalTime, stats.NumberOfRides));
					stats.Level = GetLevel(stats.CurrentPoints);

					var foundStat = await _context.UserStats.Include(userStats => userStats.Level).FirstOrDefaultAsync(e => e.Owner.Id == user.Id);

					if (foundStat == null)
					{
						stats.Owner = user;
						stats.IsNewLevel = true;

						_context.UserStats.Add(stats);
					}
					else
					{
						foundStat.Owner = user;
						foundStat.TotalDistance = stats.TotalDistance;
						foundStat.TotalTime = stats.TotalTime;
						foundStat.CurrentPoints = stats.CurrentPoints;
						if (foundStat.Level.Title != stats.Level.Title)
						{
							foundStat.IsNewLevel = true;
						}
						foundStat.Level = stats.Level;
						foundStat.IsNewLevel = stats.IsNewLevel;
						_context.UserStats.Update(foundStat);
					}
					await _context.SaveChangesAsync();

					return stats;
				}
				throw new Exception("Failed to fetch Strava stats");

			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
		return null;
	}

	private UserLevel GetLevel(long score)
	{
		var allLevels = UserLevel.GetLevels();
		return FindLevelByScore(allLevels, score);
	}

	private UserLevel FindLevelByScore(List<UserLevel> allLevels, long score)
	{
		for (int i = allLevels.Count - 1; i >= 0; i--)
		{
			if (score >= allLevels[i].RequiredPoints)
			{
				return allLevels[i];
			}
		}

		// If the user's points are below the lowest threshold, return the lowest level
		return allLevels[0];
	}

	public UserStats ParseJsonResponse(string jsonResponse)
	{
		try
		{
			JObject responseObj = JObject.Parse(jsonResponse);

			UserStats stats = new UserStats();
			stats.NumberOfRides = (int)responseObj.SelectToken("all_ride_totals.count");
			stats.TotalDistance = (double)responseObj.SelectToken("all_ride_totals.distance");
			stats.TotalTime = (int)responseObj.SelectToken("all_ride_totals.moving_time");
			stats.EvaluationTotal = (double)responseObj.SelectToken("all_ride_totals.elevation_gain");

			return stats;
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error while parsing the JSON: {e}");
			throw;
		}
	}

}
