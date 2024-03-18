namespace FitnessProject.Web.Models
{
     public class Nutrition
     {
          public int Id { get; set; }
          public string Name { get; set; }
          public double Protein { get; set; }
          public double Carbohydrate { get; set; }
          public double Fat { get; set; }
          public double EnergyValue { get; set; }
     }
}