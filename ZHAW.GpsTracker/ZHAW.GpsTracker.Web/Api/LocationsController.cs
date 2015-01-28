using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Ninject;
using ZHAW.GpsTracker.Services;
using ZHAW.GpsTracker.Web.Hubs;

namespace ZHAW.GpsTracker.Web.Api
{
    public class LocationsController : ApiController
    {
        private readonly IPositionService _positionService;

        public LocationsController(IPositionService positionService)
        {          
            _positionService = positionService;
        }

        // GET api/locations/x5fVk8z
        public IEnumerable<Location> Get(string sessionKey)
        {
            return _positionService.GetLatestPositionsForSession(sessionKey)
                                   .Select(x => new Location
                                    {
                                        Username = x.User.Name,
                                        Lat = double.Parse(x.Latitude),
                                        Lng = double.Parse(x.Longitude),
                                        Speed = x.Speed
                                    });
        }
    }
}
