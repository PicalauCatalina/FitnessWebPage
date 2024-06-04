using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.BusinessLogic.Migrations;
namespace FitnessProject.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FitnessDbContext, Configuration>());
            using (var context = new FitnessDbContext())
            {
                context.Database.Initialize(force: true);
            }
        }
    }
}