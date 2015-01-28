using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Services
{
    public interface ISessionService
    {
        Session CreateGetSession(string sessionKey);
    }
}