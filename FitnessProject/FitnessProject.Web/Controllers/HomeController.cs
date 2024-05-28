using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Domain.Entities.Workout;
using FitnessProject.Web.Extensions;
using FitnessProject.Web.Filters;
using FitnessProject.Web.Models;
using Newtonsoft.Json;

namespace FitnessProject.Web.Controllers
{
    
    [UserMod]
    public class HomeController : BaseController
    {
        private readonly IProgressManager _progressManager;
        private readonly INutritionService _nutritionService;
        private readonly IWorkoutService _workoutService;
        private GlobalModel model;

        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _progressManager = bl.GetProgressManagerBL();
            _nutritionService = bl.GetNutritionServiceBL();
            _workoutService = bl.GetWorkoutServiceBL();
            model = new GlobalModel();
            model.User = new UserMinimal();
            model.NutritionList = new List<Nutrition>();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            ViewBag.Role = "User";
            model.User = System.Web.HttpContext.Current.GetMySessionObject();
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login" || model.User == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<UProgressData> progressData = _progressManager.RestoreProgress(14, model.User.Id);
            model.ProgressList = new List<UserProgress>();

            foreach (var item in progressData)
            {
                var userProgress = new UserProgress
                {
                    Date = item.Date,
                    WorkoutDone = item.WorkoutDone,
                    SleepHours = item.SleepHours
                };

                if (item.NutitionList != null)
                {
                    userProgress.NutritionList = JsonConvert.DeserializeObject<List<NutritionDataMinimal>>(item.NutitionList);
                }

                model.ProgressList.Add(userProgress);
            }

            List<NutritionData> nutritionList = _nutritionService.GetNutritionList();
            foreach (var item in nutritionList)
            {
                model.NutritionList.Add(new Nutrition
                {
                    Id = item.Id,
                    Name = item.Name,
                    Protein = item.Protein,
                    Carbohydrate = item.Carbohydrate,
                    Fat = item.Fat,
                    EnergyValue = item.EnergyValue
                });
            }

            for (int i = 0; i < model.ProgressList.Count; i++)
            {
                double calories = 0;
                if (model.ProgressList[i].NutritionList != null)
                {
                    for (int j = 0; j < model.ProgressList[i].NutritionList.Count; j++)
                    {
                        NutritionData nutritionItem = _nutritionService.GetNutrition(model.ProgressList[i].NutritionList[j].Id);
                        calories +=
                            (nutritionItem.Protein * 4 + nutritionItem.Carbohydrate * 4 + nutritionItem.Fat * 9) *
                            model.ProgressList[i].NutritionList[j].Quantity;
                    }
                }

                model.ProgressList[i].Total = new Nutrition();
                model.ProgressList[i].Total.EnergyValue = Math.Round(calories / 100, 2);
            }

            var today = DateTime.Now.ToString("dd/MM/yyyy");
            if (model.ProgressList.Count == 0 ||
                !string.Equals(model.ProgressList[0].Date, today, StringComparison.OrdinalIgnoreCase))
            {
                model.ProgressList.Insert(0, new UserProgress
                {
                    Date = today,
                    Total = new Nutrition()
                    {
                        EnergyValue = 0,
                        Protein = 0,
                        Fat = 0,
                        Carbohydrate = 0
                    }
                });
            }

            if (model.User.WorkoutPacketId != null && model.User.WorkoutPacketId != 0)
            {
                WorkoutData workoutItem = _workoutService.GetWorkout((int)model.User.WorkoutPacketId);
                model.Workout = new Workout()
                {
                    Id = workoutItem.Id,
                    PacketName = workoutItem.PacketName,
                    Json = workoutItem.Json,
                    WorkoutPacket = JsonConvert.DeserializeObject<WorkoutPacket>(workoutItem.Json)
                };
            }

            model.User.CalculateBMR();
            model.User.CalculateDailyCaloricIntake();
            model.User.CalculateSleepHoursTarget();

            return View(model);
        }
    }
}