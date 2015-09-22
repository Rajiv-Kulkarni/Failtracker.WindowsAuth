using System.Web.Mvc;
using System.Web.Security;
using Heroic.Web.IoC;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public class CustomRoleProviderRegistry : Registry
    {
        public CustomRoleProviderRegistry()
        {
            For<IFilterProvider>().Use(new StructureMapFilterProvider());

            Policies.SetAllProperties(x =>
                x.Matching(p =>
                (p.DeclaringType.CanBeCastTo(typeof(RoleProvider))) &&
                p.DeclaringType.Namespace.StartsWith("ISAT.Admin.Test.Web") &&
                !p.PropertyType.IsPrimitive &&
                p.PropertyType != typeof(string)));
        }
    }
}