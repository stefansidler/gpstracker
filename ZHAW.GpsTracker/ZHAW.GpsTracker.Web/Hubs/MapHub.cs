using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ZHAW.GpsTracker.Data;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Web.Hubs
{
    public class MapHub : Hub
    {
        public void PropagatePosition(Location location, string sessionKey)
        {
            if (location.Lat == 0.0 || location.Lng == 0.0)
                return;

            // Add user to SignalR group (current session)
            Groups.Add(Context.ConnectionId, sessionKey);

            using (var dbContext = new TrackerContext())
            {
                Session currentSession = dbContext.Sessions.Include(x => x.Users).Include(x => x.Users.Select(y => y.Positions)).SingleOrDefault(x => x.Key == sessionKey) ??
                                     dbContext.Sessions.Add(new Session
                                     {
                                         Key = sessionKey,
                                         Name = sessionKey,
                                         Users = new List<User>()
                                     });

                User currentUser = currentSession.Users.SingleOrDefault(x => x.Name == location.Name || x.Name == Context.ConnectionId) ??
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

                var latestLocationsOfCurrentSession = currentSession.Users.ToList().Select(x => x.Positions.OrderByDescending(y => y.Timestamp).First());

                Clients.Group(sessionKey)
                    .updatePosition(
                        latestLocationsOfCurrentSession.Select(
                            x =>
                                new Location
                                {
                                    Name = x.User.Name,
                                    Lat = double.Parse(x.Latitude),
                                    Lng = double.Parse(x.Longitude),
                                    Speed = x.Speed
                                }));
            }
        }
    }
}