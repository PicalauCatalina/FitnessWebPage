using System.Collections.Generic;
using FitnessProject.BusinessLogic.Core;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Entities.User;
using FitnessProject.Domain.Entities.Workout;

namespace FitnessProject.BusinessLogic
{
    public class WorkoutServiceBL : ModeratorApi, IWorkoutService
    {
        public PostResponse CreateWorkout(WorkoutData data)
        {
            return CreateWorkoutAction(data);
        }

        public WorkoutData GetWorkout(int workoutId)
        {
            return GetWorkoutAction(workoutId);
        }

        public List<WorkoutData> GetWorkoutList()
        {
            return GetWorkoutListAction();
        }

    }
}