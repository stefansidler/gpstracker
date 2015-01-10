using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ZHAW.GpsTracker.Data;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Web.Hubs
{
    public class MapHub : Hub
    {
        static readonly List<Location> Locations = new List<Location>();

        public void PropagatePosition(Location location)
        {

            using (var dbContext = new TrackerContext())
            {
                //dbContext.Users.Add(new User{Name = "Hans", Session = })
            }
            if (Locations.Any(x => x.Name == location.Name))
            {
                Locations.Remove(Locations.First(x => x.Name == location.Name));
            }

            Locations.Add(location);

            Clients.All.updatePosition(Locations);
        }
    }
}