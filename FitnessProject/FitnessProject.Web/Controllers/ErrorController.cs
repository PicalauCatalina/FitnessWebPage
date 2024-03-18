using System.Web.Mvc;

namespace FitnessProject.Web.Controllers
{
     public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error404()
        {
            return View();
        }
    }
}