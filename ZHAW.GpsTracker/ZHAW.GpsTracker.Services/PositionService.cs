using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ZHAW.GpsTracker.Data;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Services
{
    public class PositionService : IPositionService
    {
        public Position AddPosition(Position positionToAdd)
        {
            using (var dbContext = new TrackerContext())
            {
                User dbUser = dbContext.Users.Attach(positionToAdd.User);
                Session dbSession = dbContext.Sessions.Attach(dbUser.Session);
                dbUser.Session = dbSession;
                positionToAdd.User = dbUser;
                Position position = dbContext.Positions.Add(positionToAdd);
                dbContext.SaveChanges();
                return position;
            }
        }

        public IEnumerable<Position> GetLatestPositionsForSession(string sessionKey)
        {
            using (var dbContext = new TrackerContext())
            {
                return dbContext.Users.Include(x => x.Session)
                                      .Include(x => x.Positions).ToList()
                                      .Where(x => x.Session.Key == sessionKey && x.Positions != null && x.Positions.Count > 0)
                                      .Select(user => user.Positions.OrderByDescending(x => x.Timestamp).First()).ToList();
            }
        }
    }
}