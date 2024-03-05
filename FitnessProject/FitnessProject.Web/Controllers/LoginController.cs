using FitnessProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessProject.Web.Controllers
{
     public class LoginController : Controller
     {
          public ActionResult Index()
          {
               UserLogin login = new UserLogin();

               return View(login);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Index(UserLogin login)
          {
               if (ModelState.IsValid)
               {
                    Debug.WriteLine("Credential: " + login.Credential);
                    Debug.WriteLine("Password: " + login.Password);
               }
               return View();
          }
     }
}