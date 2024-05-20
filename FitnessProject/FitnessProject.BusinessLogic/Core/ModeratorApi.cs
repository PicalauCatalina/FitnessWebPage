using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;
using System.Linq;

namespace FitnessProject.BusinessLogic.Core
{
     public class ModeratorApi
     {
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
     }
}
