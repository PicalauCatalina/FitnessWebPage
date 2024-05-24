using System.Collections.Generic;
using System.Web.Mvc;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Domain.Entities.Workout;
using FitnessProject.Web.Filters;
using FitnessProject.Web.Models;
using Newtonsoft.Json;

namespace FitnessProject.Web.Controllers
{
     [ModeratorMod]
     public class WorkspaceController : Controller
     {
          private readonly INutritionService _nutritionService;
          private readonly IWorkoutService _workoutService;
          private GlobalModel model;
          public WorkspaceController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               model = new GlobalModel();
               model.User = new UserMinimal();
               _nutritionService = bl.GetNutritionServiceBL();
               _workoutService = bl.GetWorkoutServiceBL();
               
          }
          public ActionResult ManageNutrition()
          {
               ViewBag.Title = "Manage Nutrition";
               ViewBag.Role = "Moderator";
               List<NutritionData> nutritionList = _nutritionService.GetNutritionList();
               model.NutritionList = new List<Nutrition>();
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
               return View(model);
          }

          [HttpPost]
          public ActionResult ManageNutrition(GlobalModel model)
          {
               NutritionData data = new NutritionData()
               {
                    Name = model.NutritionData.Name,
                    Carbohydrate = model.NutritionData.Carbohydrate,
                    Protein = model.NutritionData.Protein,
                    Fat = model.NutritionData.Fat,
                    EnergyValue = model.NutritionData.Protein * 4 + model.NutritionData.Carbohydrate * 4 + model.NutritionData.Fat * 9
               };

               var response = _nutritionService.CreateNutritionItem(data);
               if (response.Status)
               {
                    return RedirectToAction("ManageNutrition", "Workspace");
               }
               else
               {
                    ModelState.AddModelError("", response.StatusMsg);
                    return View();
               }
          }
          
          public ActionResult ManageWorkout()
          {
               ViewBag.Title = "Manage Workout";
               ViewBag.Role = "Moderator";
               model.WorkoutList = new List<Workout>();
               model.Workout = new Workout();
               
               List<WorkoutData> workoutList = _workoutService.GetWorkoutList();
               
               foreach (var item in workoutList)
               {
                    model.WorkoutList.Add(new Workout()
                    {
                         Id = item.Id,
                         Json = item.Json,
                         PacketName = item.PacketName,
                         WorkoutPacket = JsonConvert.DeserializeObject<WorkoutPacket>(item.Json)
                    });
               }
               
               return View(model);
          }
          
          [HttpPost]
          public ActionResult ManageWorkout(Workout workout)
          {
               WorkoutData data = new WorkoutData()
               {
                    PacketName = workout.PacketName,
                    Json = workout.Json,
                    WorkoutPacket = JsonConvert.DeserializeObject<WorkoutPacket>(workout.Json)
               };

               var workoutInsert = _workoutService.CreateWorkout(data);

               return Json("true", JsonRequestBehavior.AllowGet);
          }

     }
}