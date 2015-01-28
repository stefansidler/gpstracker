using System.Collections.Generic;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Services
{
    public interface IPositionService
    {
        Position AddPosition(Position position);

        IEnumerable<Position> GetLatestPositionsForSession(string sessionKey);
    }
}