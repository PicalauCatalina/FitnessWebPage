using FitnessProject.BusinessLogic.Core;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic
{
    public class ProgressManagerBL: UserApi, IProgressManager
    {
        public PostResponse AddNutritionProgress(int NutritionID, int NutritionQuantity, int UserID)
        {
            throw new System.NotImplementedException();
        }

        public PostResponse AddWorkoutProgress(int UserID)
        {
            throw new System.NotImplementedException();
        }
    }
}