using CricketScoreSheetPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Interface
{
    public interface ILocationService
    {
        Location AddLocation(string location);
        void DeleteLocation(string id);
        IList<Location> GetLocations();
        Location GetLocation(string Id);
        bool UpdateLocation(Location location);
    }
}
