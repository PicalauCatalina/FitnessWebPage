using System.Collections.Generic;
using FitnessProject.Domain.Entities.Nutrition;

namespace FitnessProject.Web.Models
{
    public class UserProgress
    {
        public string Date { get; set; }
        public int UserId { get; set; }
        public List<NutritionDataMinimal> NutritionList { get; set; }
        public int WorkoutDone { get; set; }
        public Nutrition Total { get; set; }
        public int SleepHours { get; set; }
    }
}