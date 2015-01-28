using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ZHAW.GpsTracker.Data;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Services
{
    public class SessionService : ISessionService
    {
        public Session CreateGetSession(string sessionKey)
        {
            using (var dbContext = new TrackerContext())
            {
                Session session = dbContext.Sessions.Include(x => x.Users)
                                                    .Include(x => x.Users.Select(y => y.Positions))
                                                    .SingleOrDefault(x => x.Key == sessionKey) ??
                                                                  dbContext.Sessions.Add(new Session
                                                                  {
                                                                      Key = sessionKey,
                                                                      Name = sessionKey,
                                                                      Users = new List<User>()
                                                                  });
                dbContext.SaveChanges();
                return session;
            }
        }
    }
}
