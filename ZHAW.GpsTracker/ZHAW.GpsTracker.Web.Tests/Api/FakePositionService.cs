using System;
using System.Collections.Generic;
using ZHAW.GpsTracker.Data.Model;
using ZHAW.GpsTracker.Services;

namespace ZHAW.GpsTracker.Web.Tests.Api
{
    public class FakePositionService : IPositionService
    {
        public Position AddPosition(Position position)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Position> GetLatestPositionsForSession(string sessionKey)
        {
            return new List<Position> {new Position
            {
                Id = 1,
                Latitude = "123",
                Longitude = "456",
                Speed = 100,
                Timestamp = DateTime.Now,
                User = new User { Name = "John" }
            }};
        }
    }
}