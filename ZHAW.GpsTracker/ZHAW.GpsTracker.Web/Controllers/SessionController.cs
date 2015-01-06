using System.Web.Mvc;

namespace ZHAW.GpsTracker.Web.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index(string sessionKey)
        {
            // TODO: Get session from db
            return View();
        }
    }
}