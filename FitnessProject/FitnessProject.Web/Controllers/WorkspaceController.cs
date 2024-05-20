using System.Collections.Generic;
using System.Web.Mvc;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Web.Models;

namespace FitnessProject.Web.Controllers
{
     public class WorkspaceController : Controller
     {
          private readonly INutritionService _nutritionService;
          private GlobalModel model;
          public WorkspaceController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               model = new GlobalModel();
               model.User = new UserMinimal();
          }
          public ActionResult ManageNutrition()
          {
               ViewBag.Title = "Manage Nutrition";
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

     }
}