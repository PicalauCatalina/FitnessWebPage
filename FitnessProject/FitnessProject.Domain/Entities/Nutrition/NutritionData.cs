﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.Domain.Entities.Nutrition
{
     public class NutritionData
     {
          public int Id { get; set; }
          public string Name { get; set; }
          public double Protein { get; set; }
          public double Carbohydrate { get; set; }
          public double Fat { get; set; }
          public double EnergyValue { get; set; }

     }
}
