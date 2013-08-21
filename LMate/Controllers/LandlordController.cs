using System.Web.Mvc;

namespace LMate.Controllers
{
    public class LandlordController : Controller
    {
        //
        // GET: /Landlord/

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
