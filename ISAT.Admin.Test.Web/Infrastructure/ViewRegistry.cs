using System.Web.Mvc;
using Heroic.Web.IoC;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public class ViewRegistry : Registry
    {
        public ViewRegistry()
        {
            For<IFilterProvider>().Use(new StructureMapFilterProvider());

            Policies.SetAllProperties(x =>
                x.Matching(p =>
                    (p.DeclaringType.CanBeCastTo(typeof(WebViewPage<>)) || p.DeclaringType.CanBeCastTo(typeof(WebViewPage))) &&
                    p.DeclaringType.Namespace.StartsWith("ISAT.Admin.Test.Web") &&
                    !p.PropertyType.IsPrimitive &&
                    p.PropertyType != typeof(string)));
        }
    }
}