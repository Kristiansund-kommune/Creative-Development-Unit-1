using System;
using System.Collections.Generic;
using System.Linq;


using Core.Concepts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Concepts.Repository
{
	public interface IBikeStationRepository : IDisposable
	{
		IEnumerable<BikeStation> GetStations(Func<BikeStation, bool> filter = null);

	}
    public class BikeStationRepository : IBikeStationRepository, IDisposable
    {
	    private StationsContext context;
	    private bool disposed = false;
        public BikeStationRepository(StationsContext context)
        {
	        this.context = context;
        }

        public IEnumerable<BikeStation> GetStations(Func<BikeStation, bool> filter = null)
        {
            if (filter != null)
            {
	            return context.BikeStations.Include(e => e.BikeStationDocks).Where(filter).ToList();
            }
            return context.BikeStations.Include(e => e.BikeStationDocks).ToList();
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
