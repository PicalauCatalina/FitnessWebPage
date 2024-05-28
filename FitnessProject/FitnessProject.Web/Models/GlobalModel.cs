using System.Collections.Generic;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.Web.Models
{
    public class GlobalModel
    {
        public UserMinimal User { get; set; }
        public Nutrition NutritionData { get; set; }
        public List<Nutrition> NutritionList { get; set; }

        public Workout Workout { get; set; }
        public List<Workout> WorkoutList { get; set; }

        public List<UserProgress> ProgressList { get; set; }

        public string Option { get; set; }
        public int FoodQuantity { get; set; }
        public int SleepHours { get; set; }
    }
}