using System.Web.Mvc;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Domain.Enums;
using FitnessProject.Web.Extensions;
using FitnessProject.Web.Filters;
using FitnessProject.Web.Models;

namespace FitnessProject.Web.Controllers
{
     [AuthorizeMod]
     public class ProfileController : BaseController
     {
          private GlobalModel model;

          public ProfileController()
          {
               model = new GlobalModel();
               model.User = new UserMinimal();
          }

          public ActionResult Index()
          {
               ViewBag.Title = "Profile";
               model.User = System.Web.HttpContext.Current.GetMySessionObject();
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return RedirectToAction("Index", "Login");
               };
               if (model.User.Level == URole.Admin)
               {
                    ViewBag.Role = "Admin";
               }
               else if (model.User.Level == URole.User)
               {
                    ViewBag.Role = "User";
               }
               model.User.CalculateBMR();
               return View(model);
          }
     }
}