using System.Collections.Generic;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic.Interfaces
{
    public interface INutritionService
    {
        PostResponse CreateNutritionItem(NutritionData data);
        List<NutritionData> GetNutritionList();
        NutritionData GetNutrition(int nutritionId);
    }
}