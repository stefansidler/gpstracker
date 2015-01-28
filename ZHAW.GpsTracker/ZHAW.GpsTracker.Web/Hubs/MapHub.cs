using System;
using System.Linq;
using System.Web.WebPages;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ZHAW.GpsTracker.Data.Model;
using ZHAW.GpsTracker.Services;

namespace ZHAW.GpsTracker.Web.Hubs
{
    public class MapHub : Hub
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IPositionService _positionService;

        public MapHub(ISessionService sessionServcie, IUserService userService, IPositionService positionService)
        {
            _sessionService = sessionServcie;
            _userService = userService;
            _positionService = positionService;
        }

        public void PropagatePosition(Location location, string sessionKey)
        {
            if (location.Lat == 0.0 || location.Lng == 0.0)
                throw new ArgumentException("Location is empty");

            // Add user to SignalR group (current session)
            GetGroupManager().Add(GetContext().ConnectionId, sessionKey);

            Session currentSession = _sessionService.CreateGetSession(sessionKey);

            User currentUser = currentSession.Users.SingleOrDefault(x => x.Name == location.Username) ??
                               _userService.CreateUser(location.Username, currentSession);

            AddCurrentPosition(location, currentUser);

            // Send latest locations to all clients in current session
            var latestLocationsOfCurrentSession = _positionService.GetLatestPositionsForSession(sessionKey);
            Clients.Group(sessionKey)
                .updatePosition(
                    latestLocationsOfCurrentSession.Select(
                        x => new Location
                             {
                                 Username = x.User.Name,
                                 Lat = double.Parse(x.Latitude),
                                 Lng = double.Parse(x.Longitude),
                                 Speed = x.Speed
                             }));
        }

        private void AddCurrentPosition(Location location, User currentUser)
        {
            _positionService.AddPosition(new Position
            {
                Latitude = location.Lat.ToString(),
                Longitude = location.Lng.ToString(),
                Speed = location.Speed,
                Timestamp = DateTime.Now,
                User = currentUser
            });
        }

        protected virtual IGroupManager GetGroupManager()
        {
            return Groups;
        }

        protected virtual HubCallerContext GetContext()
        {
            return Context;
        }
    }
}