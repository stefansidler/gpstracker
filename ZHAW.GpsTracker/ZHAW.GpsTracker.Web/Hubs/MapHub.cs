using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using ZHAW.GpsTracker.Data;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Web.Hubs
{
    public class MapHub : Hub
    {
        public void PropagatePosition(Location location, string sessionKey)
        {
            // Add user to SignalR group (current session)
            Groups.Add(Context.ConnectionId, sessionKey);

            using (var dbContext = new TrackerContext())
            {
                Session currentSession = dbContext.Sessions.SingleOrDefault(x => x.Key == sessionKey) ??
                                     dbContext.Sessions.Add(new Session
                                     {
                                         Key = sessionKey,
                                         Name = sessionKey
                                     });

                User currentUser = currentSession.Users.SingleOrDefault(x => x.Name == location.Name) ??
                                   dbContext.Users.Add(new User
                                   {
                                       Name = location.Name ?? Context.ConnectionId,
                                       Session = currentSession
                                   });

                dbContext.Positions.Add(new Position
                {
                    Latitude = location.Lat.ToString(),
                    Longitude = location.Lng.ToString(),
                    Speed = location.Speed,
                    Timestamp = DateTime.Now,
                    User = currentUser
                });
                dbContext.SaveChanges();

                var latestLocationsOfCurrentSession = currentSession.Users.Select(x => x.Positions.OrderByDescending(y => y.Timestamp).Last());
                Clients.Group(sessionKey).updatePosition(latestLocationsOfCurrentSession);
            }
        }
    }
}