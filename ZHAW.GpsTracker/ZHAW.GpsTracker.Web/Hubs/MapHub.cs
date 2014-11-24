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
        private Timer _testTimer = new Timer(100);

        static readonly List<Location> Locations = new List<Location>();

        public MapHub()
        {
            _testTimer.Elapsed += (sender, args) => Clients.All.test(DateTime.Now.ToString());
            _testTimer.Start();
        }

        public IEnumerable<Location> GetLocations()
        {
            return Locations;
        }

        public void AddLocation(string name, double latitude, double longitute, double speed)
        {
            var location = new Location { Name = name, Lat = latitude, Lng = longitute, Speed = speed };
            if (Locations.Any(x => x.Name == location.Name))
            {
                Locations.Remove(Locations.First(x => x.Name == location.Name));
            }

            Locations.Add(location);

            Clients.All.addLocations(Locations);
        }
    }
}