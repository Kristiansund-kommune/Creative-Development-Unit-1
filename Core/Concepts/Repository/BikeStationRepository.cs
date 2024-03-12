using System.Collections.Generic;
using Core.Concepts.Entities;

namespace Core.Concepts.Repository
{
    public class BikeStationRepository
    {
        private readonly List<BikeStation> bikeStations;

        public BikeStationRepository()
        {
            bikeStations = new List<BikeStation>();
        }

        public IEnumerable<BikeStation> GetAllBikeStations()
        {
            return bikeStations;
        }

        public void AddBikeStation(BikeStation station)
        {
            bikeStations.Add(station);
        }

        public bool RemoveBikeStation(BikeStation station)
        {
            return bikeStations.Remove(station);
        }
    }
}
