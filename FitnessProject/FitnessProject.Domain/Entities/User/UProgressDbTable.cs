using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessProject.Domain.Entities.User
{
    public class UProgressDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
          
        [Display(Name = "Date")]
        public string Date { get; set; }
        [Display(Name = "User ID")]
        public int UserId { get; set; }
        [Display(Name = "Workout Done")]
        public int WorkoutDone { get; set; }
        [Display(Name = "Nutrition List")]
        public string NutritionList { get; set; }
        [Display(Name = "Sleep Hours")]
        public int SleepHours { get; set; }
    }
}