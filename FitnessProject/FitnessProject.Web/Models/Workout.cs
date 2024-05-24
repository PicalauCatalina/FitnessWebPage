using FitnessProject.Domain.Entities.Workout;

namespace FitnessProject.Web.Models
{
    
    public class Workout
    {
        public int Id { get; set; }
        public string PacketName { get; set; }
        public string Json { get; set; }
        public WorkoutPacket WorkoutPacket { get; set; }
    }
}