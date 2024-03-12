using Core.Concepts.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Concepts.Repository
{
	public interface IUserRepository : IDisposable
	{
		Task<User?> GetUserById(int id);
		Task<User?> GetUserByEmail(string email);
	}

	public class UserRepository : IUserRepository, IDisposable
	{
		private bool disposed = false;
		private readonly StationsContext _context;

		public UserRepository(StationsContext context)
		{
			_context = context;
		}

		public async Task<User?> GetUserById(int id)
		{

			return await _context.Users.FindAsync(id);
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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
