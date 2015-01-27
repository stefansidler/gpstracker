using System;
using System.Web.Mvc;
using ZHAW.GpsTracker.Web.Helper;

namespace ZHAW.GpsTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.SessionKey = Base36.Encode((ulong) DateTime.Now.Ticks);

            return View();
        }
    }
}
