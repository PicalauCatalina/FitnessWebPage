using System.Collections.Generic;
using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;
using System.Linq;
using FitnessProject.Domain.Entities.Workout;

namespace FitnessProject.BusinessLogic.Core
{
     public class ModeratorApi
     {
          // [Nutrition methods]
          public PostResponse CreateNutritionItemAction(NutritionData data)
          {
               NutritionDbTable item;
               using (var db = new FitnessDbContext())
               {
                    item = db.Nutrition.FirstOrDefault(x => x.Name == data.Name);
               }
               if (item != null)
               {
                    return new PostResponse { Status = false, StatusMsg = "The item already exists" };
               }

               if (data.Name == null || data.Carbohydrate == 0 || data.EnergyValue == 0 || data.Fat == 0 || data.Protein == 0)
               {
                    return new PostResponse { Status = false, StatusMsg = "You need to fill all the fields" };
               }
               item = new NutritionDbTable()
               {
                    Name = data.Name,
                    Protein = data.Protein,
                    Carbohydrate = data.Carbohydrate,
                    Fat = data.Fat,
                    EnergyValue = data.EnergyValue
               };
               using (var db = new FitnessDbContext())
               {
                    db.Nutrition.Add(item);
                    db.SaveChanges();
               }
               return new PostResponse { Status = true };
          }
          
          public List<NutritionData> GetNutritionListAction()
          {
               List<NutritionData> foodList = new List<NutritionData>();
               using (var db = new FitnessDbContext())
               {
                    var foodDbList = db.Nutrition.ToList();
                    foreach (var food in foodDbList)
                    {
                         foodList.Add(new NutritionData
                         {
                              Id = food.Id,
                              Name = food.Name,
                              Carbohydrate = food.Carbohydrate,
                              Protein = food.Protein,
                              Fat = food.Fat,
                              EnergyValue = food.EnergyValue
                         });
                    }
               }
               return foodList;
          }
          
          public NutritionData GetNutritionAction(int nutritionId)
          {
               NutritionDbTable result;
               using (var db = new FitnessDbContext())
               {
                    result = db.Nutrition.FirstOrDefault(u => u.Id == nutritionId);
               }
               return new NutritionData
               {
                    Id = result.Id,
                    Name = result.Name,
                    Carbohydrate = result.Carbohydrate,
                    Protein = result.Protein,
                    Fat = result.Fat,
                    EnergyValue = result.EnergyValue
               };
          }
          
          
          // [Workout methods]
          public PostResponse CreateWorkoutAction(WorkoutData data)
          {
               WorkoutDbTable item;
               using (var db = new FitnessDbContext())
               {
                    item = db.Workout.FirstOrDefault(x => x.PacketName == data.PacketName);
               }
               if (item != null)
               {
                    return new PostResponse { Status = false, StatusMsg = "The packet already exists" };
               }

               if (data.PacketName == null || data.Json == null)
               {
                    return new PostResponse { Status = false, StatusMsg = "You need to fill all the fields" };
               }
               item = new WorkoutDbTable()
               {
                    PacketName = data.PacketName,
                    Json = data.Json
               };
               using (var db = new FitnessDbContext())
               {
                    db.Workout.Add(item);
                    db.SaveChanges();
               }
               return new PostResponse { Status = true };
          }
          
          public List<WorkoutData> GetWorkoutListAction()
          {
               List<WorkoutData> workoutList = new List<WorkoutData>();
               using (var db = new FitnessDbContext())
               {
                    var workoutDbList = db.Workout.ToList();
                    foreach (var workout in workoutDbList)
                    {
                         workoutList.Add(new WorkoutData
                         {
                              Id = workout.Id,
                              PacketName = workout.PacketName,
                              Json = workout.Json,
                         });
                    }
               }
               return workoutList;
          }
          
     }
}
