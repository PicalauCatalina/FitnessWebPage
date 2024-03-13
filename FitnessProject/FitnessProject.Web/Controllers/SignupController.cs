using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.User;
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
          private readonly ISession _session;
          public SignupController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
          }
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
                    USignupData data = new USignupData
                    {
                         Username = signup.Username,
                         Email = signup.Email,
                         Password = signup.Password,
                         ConfirmPassword = signup.ConfirmPassword,
                         Height = signup.Height,
                         Weight = signup.Weight,
                         WeightTarget = signup.WeightTarget,
                         WorkoutPacketId = signup.WorkoutPacketId,
                         Age = signup.Age,
                         Gender = signup.Gender,
                         LoginIp = Request.UserHostAddress,
                         LoginDateTime = DateTime.Now
                    };

                    var userSignup = _session.UserSignUp(data);
                    if (userSignup.Status)
                    {
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", userSignup.StatusMsg);
                    }
               }
               return View();
          }
     }
}