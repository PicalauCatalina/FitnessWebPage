using FitnessProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessProject.Web.Controllers
{
     public class SignupController : Controller
     {
          public ActionResult Index()
          {
               UserSignup signup = new UserSignup();

               return View(signup);
          }

          [HttpPost]
          public ActionResult Index(UserSignup signup)
          {
               if (ModelState.IsValid)
               {
                    Debug.WriteLine("Email: " + signup.Email);
                    Debug.WriteLine("Username: " + signup.Username);
                    Debug.WriteLine("Password: " + signup.Password);
                    Debug.WriteLine("ConfirmPassword: " + signup.ConfirmPassword);
                    Debug.WriteLine("Height: " + signup.Height);
                    Debug.WriteLine("Weight: " + signup.Weight);
                    Debug.WriteLine("WeightTarget: " + signup.WeightTarget);
                    Debug.WriteLine("Age: " + signup.Age);
               }
               return View();
          }
     }
}