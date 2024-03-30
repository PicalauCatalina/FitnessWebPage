using System.Web;
using FitnessProject.Domain.Entities.User;
using FitnessProject.BusinessLogic.Core;
using FitnessProject.BusinessLogic.Interfaces;

namespace FitnessProject.BusinessLogic
{
     public class SessionBL: UserApi, ISession
     {
          public PostResponse UserLogin(ULoginData data)
          {
               return UserLoginAction(data);
          }

          public PostResponse UserSignUp(USignupData data)
          {
               return UserSignupAction(data);
          }
          
          public HttpCookie GenCookie(string loginCredential)
          {
               return UserGenCookies(loginCredential);
          }

          public UserMinimal GetUserByCookie(string apiCookieValue)
          {
               return GetUserDataByCookie(apiCookieValue);
          }
     }
}
