using System.Web;
using FitnessProject.Domain.Entities.User;

namespace FitnessProject.BusinessLogic.Interfaces
{
     public interface ISession
     {
          PostResponse UserLogin(ULoginData data);
          PostResponse UserSignUp(USignupData data);
          HttpCookie GenCookie(string loginCredential);
          UserMinimal GetUserByCookie(string apiCookieValue);
     }
}
