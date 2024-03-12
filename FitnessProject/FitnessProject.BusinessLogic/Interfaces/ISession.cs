using FitnessProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.BusinessLogic.Interfaces
{
     public interface ISession
     {
          PostResponse UserLogin(ULoginData data);
          PostResponse UserSignUp(USignupData data);
     }
}
