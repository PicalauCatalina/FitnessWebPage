using System.Collections.Generic;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic.Interfaces
{
    public interface IProgressManager
    {
        PostResponse AddNutritionProgress(int nutritionId, int nutritionQuantity, int UserID);
        PostResponse AddWorkoutProgress(int userId);
        PostResponse AddSleepProgress(int sleepHours, int userId);
        List<UProgressData> RestoreProgress(int numberOfDays, int userId);
    }
}