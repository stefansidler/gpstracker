using ZHAW.GpsTracker.Data;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Services
{
    public class UserService : IUserService
    {
        public User CreateUser(string name, Session session)
        {
            using (var dbContext = new TrackerContext())
            {
                Session dbSession = dbContext.Sessions.Attach(session);
                User user = dbContext.Users.Add(new User
                            {
                                Name = name,
                                Session = dbSession
                            });
                dbContext.SaveChanges();
                return user;
            }
        }
    }
}