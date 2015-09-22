using System;
using System.Collections.Generic;
using AD = System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ISAT.Admin.Test.Web.Data;
using ISAT.Admin.Test.Web.Domain;
using ISAT.Admin.Test.Web.Filters;
using ISAT.Admin.Test.Web.Infrastructure;

namespace ISAT.Admin.Test.Web.Controllers
{
    public class HomeController : FailTrackerController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly string _displayName;

        public HomeController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            _displayName = (_currentUser.Me != null) ? _currentUser.Me.MyName : "";
        }

        //[AuthorizeAccess(Roles = "Admin")]
        [AuthorizeAccess(ApplicationRoles.Admin, ApplicationRoles.User)]
        public ActionResult Index()
        {
            //var name = (_currentUser.Me != null) ? _currentUser.Me.MyName : "";
            ////var id = (_currentUser.Me != null) ? _currentUser.Me.Id : "";
            ////var roles = _currentUser.Me?.MyRoles;
            bool isAdmin = User.IsInRole("Admin");
            //string[] roles = Roles.GetRolesForUser(_currentUser.Me.Id); //Need a custom RoleProvider to do this
            //////bool isAdmin = _currentUser.IsUserInRole("Admin");
            ViewBag.UserName = _displayName;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}