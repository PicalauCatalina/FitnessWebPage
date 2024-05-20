using System.Collections.Generic;
using FitnessProject.BusinessLogic.Core;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic
{
    public class NutritionServiceBL : ModeratorApi, INutritionService
    {
        public PostResponse CreateNutritionItem(NutritionData data)
        {
            return CreateNutritionItemAction(data);
        }

        public List<NutritionData> GetNutritionList()
        {
            return GetNutritionListAction();
        }

        public NutritionData GetNutrition(int nutritionId)
        {
            return GetNutritionAction(nutritionId);
        }
    }
}