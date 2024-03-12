using Microsoft.EntityFrameworkCore;
using Core.Concepts.Entities;

namespace Core.Concepts.Repository;

public interface IUserStatsRepository: IDisposable
{
	public Task<UserStats> UpdateUserStats(int userId);

}

public class UserStatsRepository : IUserStatsRepository
{
	private bool disposed = false;
	private readonly StationsContext _context;
	private readonly IRidesDbRepository _ridesDbRepository;
	private readonly IUserRepository _userRepository;

	public UserStatsRepository(StationsContext context, IRidesDbRepository ridesDbRepository, IUserRepository userRepository)
	{
		_context = context;
		_ridesDbRepository = ridesDbRepository;
		_userRepository = userRepository;

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
				stats.CurrentPoints = Convert.ToInt64(ExperienceCalculation.CalculateTotalEXP(50	, 15, (double) ride.ActivityDistance, 15.3));
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
}
