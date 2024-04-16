using System.Collections.Generic;
using FitnessProject.BusinessLogic.Core;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic
{
    public class NutritionServiceBL : ModeratorApi, INutritionService
    {
        public PostResponse AddNutritionProgress(int nutritionId, int nutritionQuantity, int userId)
        {
            throw new System.NotImplementedException();
        }

        public PostResponse CreateNutritionItem(NutritionData data)
        {
            throw new System.NotImplementedException();
        }

        public List<NutritionData> GetNutritionList()
        {
            throw new System.NotImplementedException();
        }

        public NutritionData GetNutrition(int nutritionId)
        {
            throw new System.NotImplementedException();
        }
    }
}