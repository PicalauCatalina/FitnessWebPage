using FitnessProject.Domain.Enums;
using System;

namespace FitnessProject.Domain.Entities.User
{
     public class UserMinimal
     {
          public int Id { get; set; }
          public string Username { get; set; }
          public string Email { get; set; }
          public string Gender { get; set; }
          public int Age { get; set; }
          public int Height { get; set; }
          public float Weight { get; set; }
          public URole Level { get; set; }
          public float WeightTarget { get; set; }
          public double DailyCaloricIntake { get; set; }
          public double BMR { get; set; }
          public int SleepHoursTarget { get; set; }
          public int? WorkoutPacketId { get; set; }

          public void CalculateBMR()
          {
               if (this.Gender == "Male")
               {
                    this.BMR = 88.362 + (13.397 * this.Weight) + (4.799 * this.Height) - (5.677 * this.Age);
               }

               if (this.Gender == "Female")
               {
                    this.BMR = 447.593 + (9.247 * this.Weight) + (3.098 * this.Height) - (4.330 * this.Age);
               }
          }
          public void CalculateDailyCaloricIntake()
          {
               this.DailyCaloricIntake = this.BMR * 1.55;

               if (this.WeightTarget > this.Weight)
               {
                    this.DailyCaloricIntake += 500;
               }
               else if (this.WeightTarget < this.Weight)
               {
                    this.DailyCaloricIntake -= 500;
               }
               this.DailyCaloricIntake = Math.Round(this.DailyCaloricIntake, 2);
          }

          public void CalculateSleepHoursTarget()
          {
               if (this.Age <= 2)
               {
                    this.SleepHoursTarget = 14;
               }
               else if (this.Age >= 3 && this.Age <= 5)
               {
                    this.SleepHoursTarget = 13;
               }
               else if (this.Age >= 6 && this.Age <= 13)
               {
                    this.SleepHoursTarget = 11;
               }
               else if (this.Age >= 14 && this.Age <= 17)
               {
                    this.SleepHoursTarget = 10;
               }
               else if (this.Age >= 18 && this.Age <= 25)
               {
                    this.SleepHoursTarget = 9;
               }
               else if (this.Age >= 26 && this.Age <= 64)
               {
                    this.SleepHoursTarget = 8;
               }
               else if (this.Age >= 65)
               {
                    this.SleepHoursTarget = 7;
               }
          }
     }
}
