namespace FitnessProject.Domain.Entities.Workout
{
    public class WorkoutData
    {
        public int Id { get; set; }
        public string PacketName { get; set; }
        public string Json { get; set; }
        public WorkoutPacket WorkoutPacket { get; set; }
    }
}