using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using ZHAW.GpsTracker.Services;
using ZHAW.GpsTracker.Web.Api;
using ZHAW.GpsTracker.Web.Hubs;

namespace ZHAW.GpsTracker.Web.Tests.Api
{
    [TestFixture]
    public class LocationsControllerTests
    {
        [Test]
        public void Get_AtLeastOneLocationExists_ReturnsLocations()
        {
            LocationsController sut = CreateSut();

            IEnumerable<Location> actual = sut.Get("");

            Assert.That(actual.Count(), Is.AtLeast(1));
        }

        [Test]
        public void Get_AtLeastOneLocationExists_MapsProperties()
        {
            LocationsController sut = CreateSut();

            Location actual = sut.Get("").First();

            Assert.That(actual.Username, Is.Not.Empty);
            Assert.That(actual.Lat, Is.Not.Null);
            Assert.That(actual.Lng, Is.Not.Null);
            Assert.That(actual.Speed, Is.Not.Null);
        }

        private LocationsController CreateSut()
        {
            return new LocationsController(new FakePositionService());
        }
    }
}