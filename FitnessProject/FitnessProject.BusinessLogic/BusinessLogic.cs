using FitnessProject.BusinessLogic.Interfaces;

namespace FitnessProject.BusinessLogic
{
     public class BusinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
          
          public INutritionService GetNutritionServiceBL()
          {
               return new NutritionServiceBL();
          }
          
          public IWorkoutService GetWorkoutServiceBL()
          {
               return new WorkoutServiceBL();
          }
          
          public IProgressManager GetProgressManagerBL()
          {
               return new ProgressManagerBL();
          }
     }
}
