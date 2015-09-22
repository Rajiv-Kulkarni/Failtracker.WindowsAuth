using ISAT.Admin.Test.Web.Data;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISAT.Admin.Test.Web.Controllers;
using ISAT.Admin.Test.Web.Domain;
using ISAT.Admin.Test.Web.Infrastructure;
using ISAT.Admin.Test.Web.Infrastructure.Alerts;
using Microsoft.Web.Mvc;

namespace ISAT.Admin.Test.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAccessAttribute : AuthorizeAttribute
    {
        public ApplicationDbContext Context { get; set; }
        public ICurrentUser CurrentUser { get; set; }

        public string AllowedRoles { get; set; }

        //public new string Roles
        //{
        //    get; set;
        //}

        public AuthorizeAccessAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            //AllowedRoles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
            Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    if (!base.AuthorizeCore(httpContext)) return false;

        //    //get the user from db
        //    var user = Context.Users.SingleOrDefault(u => u.Id == CurrentUser.Me.Id);

        //    //get user roles from db
        //    IList<string> userRolesDb = null;
        //    if (user != null)
        //    {
        //        userRolesDb = user.Roles.Select(u => u.Name).ToList();
        //    }

        //    //convert the string of roles passed in into a list
        //    //IList<string> userRolesToCheck = Roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    IList<string> userRolesToCheck = AllowedRoles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        //    //check if each of the roles in the list passed in exists in the list of roles retrieved
        //    if (userRolesToCheck.Where(role => userRolesDb != null).Any(role => userRolesDb != null && userRolesDb.Contains(role)))
        //    {
        //        return true;
        //    }

        //    //roles passed in fail
        //    return false;
        //}

        protected override void HandleUnauthorizedRequest(
        AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (CurrentUser?.Me != null)
                {
                    filterContext.Controller.ViewBag.UserName = CurrentUser.Me.MyName;
                    filterContext.Result = ((FailTrackerController)filterContext.Controller).RedirectToAction<HomeController>(a => a.Index()).WithError("You are not authorized to update data.");
                }
                //var result = new ViewResult
                //{
                //    ViewName = "NotAuthorized",
                //    MasterName = "_Layout"
                //};
                //filterContext.Result = result;
                //MattQuestion: how to redirect to the home page Index? what is the best way to handle this error?
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }
    }
}