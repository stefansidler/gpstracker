using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Ninject;
using Owin;
using ZHAW.GpsTracker.Services;
using ZHAW.GpsTracker.Web.App_Start;
using ZHAW.GpsTracker.Web.Hubs;

[assembly: OwinStartup(typeof(ZHAW.GpsTracker.Web.Startup))]
namespace ZHAW.GpsTracker.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var kernel = NinjectWebCommon.Kernel; //new StandardKernel();
            var resolver = new NinjectSignalRDependencyResolver(kernel);

            kernel.Bind(typeof (IHubConnectionContext<dynamic>)).ToMethod(context =>
                resolver.Resolve<IConnectionManager>().GetHubContext<MapHub>().Clients);
            
            var config = new HubConfiguration {Resolver = resolver};
            ConfigureSignalR(appBuilder, config);
        }

        public static void ConfigureSignalR(IAppBuilder app, HubConfiguration config)
        {
            app.MapSignalR(config);
        }
    }
}