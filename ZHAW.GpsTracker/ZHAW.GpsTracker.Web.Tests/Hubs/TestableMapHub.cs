using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NSubstitute;
using ZHAW.GpsTracker.Services;
using ZHAW.GpsTracker.Web.Hubs;

namespace ZHAW.GpsTracker.Web.Tests.Hubs
{
    public class TestableMapHub : MapHub
    {
        public TestableMapHub(ISessionService sessionServcie, IUserService userService, IPositionService positionService) 
            : base(sessionServcie, userService, positionService)
        {
            Clients = CreateClientsContext();
            Context = Substitute.For<HubCallerContext>();
            Groups = Substitute.For<IGroupManager>();
        }

        private static IHubCallerConnectionContext<dynamic> CreateClientsContext()
        {
            var clients = Substitute.For<IHubCallerConnectionContext<dynamic>>();
            ISignals signals = Substitute.For<ISignals>();
            SubstituteExtensions.Returns(clients.Group("sessionkey"), signals);
            return clients;
        }

        public interface ISignals
        {
            void updatePosition(IEnumerable<Location> locations);
        }
    }
}