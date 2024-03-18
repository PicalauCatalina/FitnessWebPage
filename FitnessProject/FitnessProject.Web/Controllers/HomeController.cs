using System.Web.Mvc;

namespace FitnessProject.Web.Controllers
{
     public class HomeController : Controller
    {
        public ActionResult Index()
          {
               ViewBag.Title = "Dashboard";
               return View();
        }
    }
}
