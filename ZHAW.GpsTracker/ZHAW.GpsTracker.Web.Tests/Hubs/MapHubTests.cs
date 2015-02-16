using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Hubs;
using NSubstitute;
using NUnit.Framework;
using ZHAW.GpsTracker.Data.Model;
using ZHAW.GpsTracker.Services;
using ZHAW.GpsTracker.Web.Hubs;

namespace ZHAW.GpsTracker.Web.Tests.Hubs
{
    [TestFixture]
    public class MapHubTests
    {
        [Test]
        public void PropagatePosition_EmptyLocation_ThrowsArgumentException()
        {
            var sut = new TestableMapHub(Substitute.For<ISessionService>(), Substitute.For<IUserService>(), Substitute.For<IPositionService>());

            Assert.Throws<ArgumentException>(() => sut.PropagatePosition(new Location(), "asdf"));
        }

        [Test]
        public void PropagatePosition_WithSessionKey_CallCreateSession()
        {
            var fakeSessionService = Substitute.For<ISessionService>();
            fakeSessionService.CreateGetSession("sessionkey").Returns(new Session { Users = new List<User>() });
            var fakeUserService = Substitute.For<IUserService>();
            var fakePositionService = Substitute.For<IPositionService>();
            fakePositionService.GetLatestPositionsForSession("sessionkey")
                .Returns(new List<Position>
                {
                    new Position {User = new User(), Latitude = "1", Longitude = "2", Speed = 0}
                });
            
            var sut = new TestableMapHub(fakeSessionService, fakeUserService, fakePositionService);

            sut.PropagatePosition(new Location { Lat = 1, Lng = 1 }, "sessionkey");

            fakeSessionService.Received(1).CreateGetSession("sessionkey");
        }

        [Test]
        public void PropagatePosition_UserDoesNotExist_CallCreateUser()
        {
            // Arrange
            var fakeSessionService = Substitute.For<ISessionService>();
            var testSession = new Session {Users = new List<User>()};
            fakeSessionService.CreateGetSession("sessionkey").Returns(testSession);
            var fakeUserService = Substitute.For<IUserService>();
            var fakePositionService = Substitute.For<IPositionService>();
            var sut = new TestableMapHub(fakeSessionService, fakeUserService, fakePositionService);

            // Act
            sut.PropagatePosition(new Location { Lat = 1, Lng = 1, Username = "username" }, "sessionkey");

            // Assert
            fakeUserService.Received(1).CreateUser("username", testSession);
        }

        [Test]
        public void PropagatePosition_UserExists_DoNotCallCreateUser()
        {
            var fakeSessionService = Substitute.For<ISessionService>();
            var testSession = new Session { Users = new List<User> { new User { Name = "username" }} };
            fakeSessionService.CreateGetSession("sessionkey").Returns(testSession);
            var fakeUserService = Substitute.For<IUserService>();
            var fakePositionService = Substitute.For<IPositionService>();

            var sut = new TestableMapHub(fakeSessionService, fakeUserService, fakePositionService);

            sut.PropagatePosition(new Location { Lat = 1, Lng = 1, Username = "username"}, "sessionkey");

            fakeUserService.Received(0).CreateUser(Arg.Any<string>(), Arg.Any<Session>());
        }

        [Test]
        public void PropagatePosition_EveryTime_AddPosition()
        {
            var fakeSessionService = Substitute.For<ISessionService>();
            var testSession = new Session { Users = new List<User> { new User { Name = "username" } } };
            fakeSessionService.CreateGetSession("sessionkey").Returns(testSession);
            var fakeUserService = Substitute.For<IUserService>();
            var fakePositionService = Substitute.For<IPositionService>();

            var sut = new TestableMapHub(fakeSessionService, fakeUserService, fakePositionService);

            sut.PropagatePosition(new Location { Lat = 1, Lng = 1, Username = "username" }, "sessionkey");

            fakePositionService.Received(1).AddPosition(Arg.Any<Position>());
        }
    }
}