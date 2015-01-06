using System.Collections.Generic;
using System.Web.Http;
using ZHAW.GpsTracker.Web.Hubs;

namespace ZHAW.GpsTracker.Web.Controllers
{
    public class LocationsController : ApiController
    {
        // GET api/locations/x5fVk8z
        public IEnumerable<Location> Get(string sessionKey)
        {
            // TODO: Get locations from db
            return new List<Location> { new Location() };
        }
    }
}
