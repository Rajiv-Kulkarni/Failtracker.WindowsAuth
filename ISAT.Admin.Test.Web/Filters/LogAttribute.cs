using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ISAT.Admin.Test.Web.Data;
using ISAT.Admin.Test.Web.Domain;
using ISAT.Admin.Test.Web.Infrastructure;

namespace ISAT.Admin.Test.Web.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private IDictionary<string, object> _parameters;
        public ApplicationDbContext Context { get; set; }
        public ICurrentUser CurrentUser { get; set; }

        public string Description { get; set; }

        public LogAttribute(string description)
        {
            Description = description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _parameters = filterContext.ActionParameters;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var description = _parameters.Aggregate(Description, (current, kvp) => current.Replace("{" + kvp.Key + "}", kvp.Value?.ToString() ?? ""));

            Context.Logs.Add(new LogAction(CurrentUser.Me, filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, description));

            Context.SaveChanges();
        }
    }
}