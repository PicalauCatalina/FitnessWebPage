using FitnessProject.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessProject.Domain.Entities.User
{
     public class UDbTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          [Display(Name = "Username")]
          [StringLength(30, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 30 characters")]
          public string Username { get; set; }

          [Required]
          [Display(Name = "Password")]
          [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters")]
          public string Password { get; set; }

          [Required]
          [Display(Name = "Email Address")]
          [StringLength(30)]
          public string Email { get; set; }

          [Required]
          [Display(Name = "Gender")]
          public string Gender { get; set; }

          [Required]
          [Display(Name = "Age")]
          public int Age { get; set; }

          [Required]
          [Display(Name = "Height")]
          public int Height { get; set; }

          [Required]
          [Display(Name = "Weight")]
          public float Weight { get; set; }

          [Required]
          [Display(Name = "Weight Target")]
          public float WeightTarget { get; set; }

          [Required]
          [Display(Name = "Workout Packet ID")]
          public int? WorkoutPacketID { get; set; }


          [DataType(DataType.Date)]
          public DateTime LastLogin { get; set; }

          [StringLength(30)]
          public string LastIp { get; set; }

          public URole Level { get; set; }

     }
}
