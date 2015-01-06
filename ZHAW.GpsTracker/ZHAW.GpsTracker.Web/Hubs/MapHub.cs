using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ZHAW.GpsTracker.Web.Hubs
{
    public class MapHub : Hub
    {
        static readonly List<Location> Locations = new List<Location>();

        public IEnumerable<Location> GetLocations()
        {
            return Locations;
        }

        public void AddLocation(Location location)
        {
            if (Locations.Any(x => x.Name == location.Name))
            {
                Locations.Remove(Locations.First(x => x.Name == location.Name));
            }

            Locations.Add(location);

            Clients.All.addLocations(Locations);
        }
    }
}