using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessProject.Domain.Entities.Workout
{
    public class WorkoutDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "PacketName")]
        public string PacketName { get; set; }

        [Required]
        [Display(Name = "Json")]
        public string Json { get; set; }
    }
}