using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.Domain.Entities.Nutrition
{
     public class NutritionDbTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          [Display(Name = "Name")]
          public string Name { get; set; }

          [Required]
          [Display(Name = "Protein")]
          public double Protein { get; set; }

          [Required]
          [Display(Name = "Carbohydrate")]
          public double Carbohydrate { get; set; }

          [Required]
          [Display(Name = "Fat")]
          public double Fat { get; set; }

          [Required]
          [Display(Name = "Energy Value")]
          public double EnergyValue { get; set; }
     }
}
