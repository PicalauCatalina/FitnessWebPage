using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FitnessProject.BusinessLogic.Interfaces;
using FitnessProject.Web.Extensions;

namespace FitnessProject.Web.Filters
{
    public class AuthorizeModAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;

        public AuthorizeModAttribute()
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
                if (profile != null)
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
                    new RouteValueDictionary(new { controller = "Home", action = "Welcome" }));
            }
        }
    }
}