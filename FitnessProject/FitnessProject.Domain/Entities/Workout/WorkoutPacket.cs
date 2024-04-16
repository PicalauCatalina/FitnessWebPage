using System.Collections.Generic;

namespace FitnessProject.Domain.Entities.Workout
{
    public class Exercise
    {
        public string Name { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public string GifLink { get; set; }
    }

    public class WorkoutDay
    {
        public string Day { get; set; }
        public List<Exercise> Exercise { get; set; }
    }

    public class WorkoutPacket
    {
        public List<WorkoutDay> Json { get; set; }
    }
}