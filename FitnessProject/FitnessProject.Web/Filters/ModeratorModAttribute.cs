using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Domain.Enums;
using FitnessProject.Web.Extensions;

namespace FitnessProject.Web.Filters
{
    public class ModeratorModAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;
        public ModeratorModAttribute()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null && profile.Level == URole.Moderator)
                {
                    Debug.WriteLine(profile.Level);
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
        }
    }
}