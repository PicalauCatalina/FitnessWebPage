using System.Collections.Generic;
using FitnessProject.BusinessLogic.Core;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic
{
    public class ProgressManagerBL: UserApi, IProgressManager
    {
        public PostResponse AddNutritionProgress(int nutritionId,int nutritionQuantity, int userId)
        {
            return AddNutrition(nutritionId, nutritionQuantity, userId);
        }

        public PostResponse AddWorkoutProgress(int userId)
        {
            return AddWorkout(userId);
        }

        public PostResponse AddSleepProgress(int sleepHours, int userId)
        {
            return AddSleep(sleepHours, userId);
        }

        public List<UProgressData> RestoreProgress(int numberOfDays, int userId)
        {
            return RestoreProgressAction(numberOfDays, userId);
        }
    }
}