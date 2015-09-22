using System.Data.Entity;
using StructureMap;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Heroic.Web.IoC;
using ISAT.Admin.Test.Web.Data;
using ISAT.Admin.Test.Web.Infrastructure;
using ISAT.Admin.Test.Web.Infrastructure.Tasks;
using ISAT.Admin.Test.Web.Migrations;

namespace ISAT.Admin.Test.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IContainer Container => StructureMapContainerPerRequestModule.Container;

        protected void Application_Start()
        {
            StructureMapContainerPerRequestModule.PreDisposeContainer += ExecuteEndTasks;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CustomRoleProviderHelper.SetCustomRoleProviderConnectionString(Server.MapPath(@"~/Web.Config"));
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            using (var container = IoC.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }

        public void Application_BeginRequest()
        {
            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void ExecuteEndTasks(IContainer nestedContainer)
        {
            foreach (var task in nestedContainer.GetAllInstances<IRunAfterEachRequest>())
            {
                task.Execute();
            }
        }

    }
}
