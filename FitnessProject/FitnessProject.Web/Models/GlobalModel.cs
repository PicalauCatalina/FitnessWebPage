using System.Collections.Generic;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.Web.Models
{
     public class GlobalModel
     {
          public UserMinimal User { get; set; }
          public Nutrition NutritionData { get; set; }
          public List<Nutrition> NutritionList { get; set; }
     }
}