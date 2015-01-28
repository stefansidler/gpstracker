using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Services
{
    public interface IUserService
    {
        User CreateUser(string name, Session session);
    }
}