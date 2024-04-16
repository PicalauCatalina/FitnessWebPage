using System.Collections.Generic;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Domain.Entities.Workout;

namespace FitnessProject.BusinessLogic.Interfaces
{
    public interface IWorkoutService
    {
        List<WorkoutData> GetWorkoutList();
        WorkoutData GetWorkout(int workoutId);
        PostResponse CreateWorkout(WorkoutData data);
    }
}