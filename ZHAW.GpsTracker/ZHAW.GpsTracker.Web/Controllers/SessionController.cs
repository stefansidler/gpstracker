using System.Web.Mvc;

namespace ZHAW.GpsTracker.Web.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index(string sessionKey)
        {
            if (string.IsNullOrWhiteSpace(sessionKey))
                return new HttpNotFoundResult();

            return View();
        }
    }
}