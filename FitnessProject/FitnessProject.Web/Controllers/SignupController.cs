using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FitnessProject.Web.Controllers
{
     public class SignupController : Controller
     {
          private readonly ISession _session;
          private readonly IWorkoutService _workoutService;
          public SignupController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
               _workoutService = bl.GetWorkoutServiceBL();
          }
          public ActionResult Index()
          {
               ViewBag.Title = "Signup";
               UserSignup model = new UserSignup();
               var workoutList = _workoutService.GetWorkoutList();
               model.WorkoutPacketIdList = new List<SelectListItem>();
               foreach (var item in workoutList)
               {
                    model.WorkoutPacketIdList.Add(new SelectListItem
                    {
                         Text = item.PacketName,
                         Value = item.Id.ToString()
                    });
               }

               model.WorkoutPacketIdList.Insert(0, new SelectListItem { Value = "", Text = "-- select packet --" });
               return View(model);
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
                         return RedirectToAction("Index", "Login");
                    }

                    ModelState.AddModelError("", userSignup.StatusMsg);
               }
               return View();
          }
     }
}