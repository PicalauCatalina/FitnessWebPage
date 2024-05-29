using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Domain.Entities.Workout;
using FitnessProject.Web.Extensions;
using FitnessProject.Web.Filters;
using FitnessProject.Web.Models;
using Newtonsoft.Json;

namespace FitnessProject.Web.Controllers
{
       [UserMod]
     public class WorkoutController : Controller
     {

          private readonly IProgressManager _progressManager;
          private readonly IWorkoutService _workoutService;
          private readonly GlobalModel _model;
          public WorkoutController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _progressManager = bl.GetProgressManagerBL();
               _workoutService = bl.GetWorkoutServiceBL();
               _model = new GlobalModel();
               _model.User = new UserMinimal();
               
          }

          public void CommonThings()
          {
               List<UProgressData> progressData = _progressManager.RestoreProgress(14, _model.User.Id);
               _model.ProgressList = new List<UserProgress>();
               foreach (var item in progressData)
               {
                    _model.ProgressList.Add(new UserProgress
                    {
                         Date = item.Date,
                         WorkoutDone = item.WorkoutDone
                    });
               }

               var today = DateTime.Now.ToString("dd/MM/yyyy");
               if (_model.ProgressList.Count == 0 || !string.Equals(_model.ProgressList[0].Date, today, StringComparison.OrdinalIgnoreCase))
               {
                    _model.ProgressList.Insert(0, new UserProgress
                    {
                         Date = today,
                         WorkoutDone = 0
                    });
               }
               
               WorkoutData workoutItem = _workoutService.GetWorkout(_model.User.WorkoutPacketId);

               _model.Workout = new Workout()
               {
                    Id = workoutItem.Id,
                    PacketName = workoutItem.PacketName,
                    Json = workoutItem.Json,
                    WorkoutPacket = JsonConvert.DeserializeObject<WorkoutPacket>(workoutItem.Json)
               };
          }

          public ActionResult Index()
          {
               ViewBag.Title = "Workout Plan";
               ViewBag.Role = "User";
               _model.User = System.Web.HttpContext.Current.GetMySessionObject();

               CommonThings();
               
               return View(_model);
          }

          public ActionResult Statistic()
          {
               ViewBag.Title = "Workout Statistic";
               ViewBag.Role = "User";
               _model.User = System.Web.HttpContext.Current.GetMySessionObject();

               CommonThings();

               return View(_model);
          }

          public ActionResult Practice()
          {
               ViewBag.Title = "Workout Practice";
               ViewBag.Role = "User";
               _model.User = System.Web.HttpContext.Current.GetMySessionObject();

               CommonThings();

               return View(_model);
          }

          [HttpPost]
          public ActionResult Practice(GlobalModel model)
          {
               ViewBag.Title = "Workout Practice";
               ViewBag.Role = "User";
               model.User = System.Web.HttpContext.Current.GetMySessionObject();
               _progressManager.AddWorkoutProgress(model.User.Id);
               return RedirectToAction("Practice");
          }

     }
}