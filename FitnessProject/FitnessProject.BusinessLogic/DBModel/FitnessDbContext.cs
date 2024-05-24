using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.Session;
using FitnessProject.Domain.Entities.User;
using System.Data.Entity;
using FitnessProject.Domain.Entities.Workout;

namespace FitnessProject.BusinessLogic.DBModel
{
     public class FitnessDbContext : DbContext
     {
          public FitnessDbContext() : base("name=Fitness")
          {
          }

          public DbSet<UDbTable> Users { get; set; }
          public virtual DbSet<SessionsDbTable> Sessions { get; set; }
          public virtual DbSet<NutritionDbTable> Nutrition { get; set; }
          public virtual DbSet<WorkoutDbTable> Workout { get; set; }
          public virtual DbSet<UProgressDbTable> Progress { get; set; }

     }
}
