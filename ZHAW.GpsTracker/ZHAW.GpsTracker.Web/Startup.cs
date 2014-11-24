using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ZHAW.GpsTracker.Web.Startup))]
namespace ZHAW.GpsTracker.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.MapSignalR();
        }
    }
}