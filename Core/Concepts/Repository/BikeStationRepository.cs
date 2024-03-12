using Core.Concepts.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Concepts.Repository
{
	public interface IBikeStationRepository : IDisposable
	{
		Task<IEnumerable<BikeStation>> GetStations(Expression<Func<BikeStation, bool>> filter = null);

	}
    public class BikeStationRepository : IBikeStationRepository, IDisposable
    {
	    private StationsContext context;
	    private bool disposed = false;
        public BikeStationRepository(StationsContext context)
        {
	        this.context = context;
        }

        public async Task<IEnumerable<BikeStation>> GetStations(Expression<Func<BikeStation, bool>>? filter = null)
        {
            if (filter != null)
            {
	            return await GetAllBikeStations().Include(e => e.BikeStationDocks).Where(filter).ToListAsync();
            }

            return await GetAllBikeStations().Include(e => e.BikeStationDocks).ToListAsync();
        }

        public IQueryable<BikeStation> GetAllBikeStations()
        {
	        return context.BikeStations.AsQueryable();
        }


        protected virtual void Dispose(bool disposing)
        {
	        if (!this.disposed)
	        {
		        if (disposing)
		        {
			        context.Dispose();
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
