using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.Domain.Entities.User
{
     public class USignupData
     {
          public string Email { get; set; }
          public string Username { get; set; }
          public string Password { get; set; }
          public string ConfirmPassword { get; set; }
          public int Height { get; set; }
          public float Weight { get; set; }
          public float WeightTarget { get; set; }
          public int? WorkoutPacketId { get; set; }
          public int Age { get; set; }
          public string Gender { get; set; }
          public string LoginIp { get; set; }
          public DateTime LoginDateTime { get; set; }
     }
}
