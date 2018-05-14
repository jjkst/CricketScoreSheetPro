using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException($"UmpireRepository is null");
        }

        public string AddLocation(string location)
        {
            if (string.IsNullOrEmpty(location)) throw new ArgumentNullException($"Umpire name is empty");
            var newLocation = new Location
            {
                Name = location,
                AddDate = DateTime.Today
            };
            var locationAdd = _locationRepository.Create(newLocation);
            return locationAdd;
        }

        public void DeleteLocation(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Umpire ID is null");
            _locationRepository.Delete(id);
        }

        public Location GetLocation(string Id)
        {
            if (string.IsNullOrEmpty(Id)) throw new ArgumentException($"Umpire ID is null");
            var location = _locationRepository.GetItem(Id);
            return location;
        }

        public IList<Location> GetLocations()
        {
            var umpires = _locationRepository.GetList();
            return umpires;
        }

        public bool UpdateLocation(Location location)
        {
            if (location == null) throw new ArgumentNullException($"UserUmpire is null");
            var updatedlocation = _locationRepository.Update(location.Id, location);
            return updatedlocation;
        }
    }
}
