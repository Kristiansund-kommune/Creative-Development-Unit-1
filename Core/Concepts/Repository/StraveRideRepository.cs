using Microsoft.EntityFrameworkCore;
using Core.Concepts.Entities;

namespace Core.Concepts.Repository;


public class StravaRideRepository : IRidesDbRepository
{
	private bool disposed = false;
	private readonly StationsContext _context;

	public StravaRideRepository(StationsContext context)
	{
		_context = context;

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

	public async Task<List<Route>> GetUserRoutes(int userId)
	{
		return await _context.Routes.Where(e => e.UserId == userId).ToListAsync();
	}
}
