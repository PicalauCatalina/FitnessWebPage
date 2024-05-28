using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Web.Extensions;
using FitnessProject.Web.Filters;
using FitnessProject.Web.Models;
using Newtonsoft.Json;

namespace FitnessProject.Web.Controllers
{
    
     [UserMod]
     public class NutritionController : Controller
     {
          private readonly IProgressManager _progressManager;
          private readonly INutritionService _nutritionService;
          public NutritionController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _progressManager = bl.GetProgressManagerBL();
               _nutritionService = bl.GetNutritionServiceBL();
          }
          public ActionResult Statistic()
          {
               ViewBag.Title = "Nutrition Statistics";
               ViewBag.Role = "User";
               GlobalModel model = new GlobalModel
               {
                    User = new UserMinimal()
               };
               model.User = System.Web.HttpContext.Current.GetMySessionObject();
               var progressData = _progressManager.RestoreProgress(14, model.User.Id);
               model.ProgressList = new List<UserProgress>();

               foreach (var item in progressData)
               {
                    if (item.NutitionList != null)
                    {
                         model.ProgressList.Add(new UserProgress
                         {
                              Date = item.Date,
                              NutritionList = JsonConvert.DeserializeObject<List<NutritionDataMinimal>>(item.NutitionList)
                         });
                    }
               }
               foreach (var p in model.ProgressList)
               {
                    double calories = 0;
                    double protein = 0;
                    double fat = 0;
                    double carbs = 0;

                    for (int j = 0; j < p.NutritionList.Count; j++)
                    {
                         var nutritionItem = _nutritionService.GetNutrition(p.NutritionList[j].Id);
                         calories += (nutritionItem.Protein * 4 + nutritionItem.Carbohydrate * 4 + nutritionItem.Fat * 9) * p.NutritionList[j].Quantity;
                         protein += nutritionItem.Protein * p.NutritionList[j].Quantity;
                         fat += nutritionItem.Fat * p.NutritionList[j].Quantity;
                         carbs += nutritionItem.Carbohydrate * p.NutritionList[j].Quantity;
                    }
                    p.Total = new Nutrition();
                    p.Total.EnergyValue = Math.Round(calories / 100, 2);
                    p.Total.Protein = Math.Round(protein / 100, 2);
                    p.Total.Fat = Math.Round(fat / 100, 2);
                    p.Total.Carbohydrate = Math.Round(carbs / 100, 2);
               }

               var today = DateTime.Now.ToString("dd/MM/yyyy");
               if (model.ProgressList.Count == 0 || !string.Equals(model.ProgressList[0].Date, today, StringComparison.OrdinalIgnoreCase))
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
               
               model.User.CalculateBMR();
               model.User.CalculateDailyCaloricIntake();

               return View(model);
          }
     }
}