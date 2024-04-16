using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic.Interfaces
{
    public interface IProgressManager
    {
        PostResponse AddNutritionProgress(int NutritionID, int NutritionQuantity, int UserID);
        PostResponse AddWorkoutProgress(int UserID);
        
    }
}