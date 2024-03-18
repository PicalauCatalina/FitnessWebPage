using FitnessProject.BusinessLogic.Interfaces;

namespace FitnessProject.BusinessLogic
{
     public class BusinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
     }
}
