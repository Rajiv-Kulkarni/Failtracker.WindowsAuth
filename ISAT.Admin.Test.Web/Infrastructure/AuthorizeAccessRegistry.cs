using System.Web.Mvc;
using Heroic.Web.IoC;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public class AuthorizeAccessRegistry : Registry
    {
        public AuthorizeAccessRegistry()
        {
            For<IFilterProvider>().Use(new StructureMapFilterProvider());

            //Policies.FillAllPropertiesOfType<ApplicationDbContext>()
            //    .Use(() => new ApplicationDbContext(ApplicationDbContext.conStr));
            Policies.SetAllProperties(x =>
                x.Matching(p =>
                (p.DeclaringType.CanBeCastTo(typeof(AuthorizeAttribute))) &&
                p.DeclaringType.Namespace.StartsWith("ISAT.Admin.Test.Web") &&
                !p.PropertyType.IsPrimitive &&
                p.PropertyType != typeof(string)));
        }
    }
}