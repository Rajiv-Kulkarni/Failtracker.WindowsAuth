using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using AutoMapper.QueryableExtensions;
using ISAT.Admin.Test.Web.Data;
using ISAT.Admin.Test.Web.Domain;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentity _identity;
        private readonly PrincipalContext _contextUser;

        //IPrincipal 
        //public IIdentity Identity { get; private set; }
        //public bool IsInRole(string role)
        //{
        //    return false;
        //}

        private ApplicationUser _user;

        public CurrentUser(IIdentity identity, ApplicationDbContext context, PrincipalContext contextUser)
        {
            _identity = identity;
            _context = context;
            _contextUser = contextUser;
            //Identity = identity;

            //MattQuestion: should this be done in PostAuthenticateRequest or AuthenticateRequest event handler in glbal.asax? or in a class derived from IRunOnEachRequest
            var user = _context.Users.SingleOrDefault(u => u.Id == _id);//retrieve user roles from the database
            var principal = new GenericPrincipal(_identity, user?.Roles.Distinct().Select(r => r.Name).ToArray());
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;//this will enable using Authorize attribute with roles
        }

        public ApplicationUser Me
        {
            get
            {
                return _user ?? (_user = _context.Users.Where(u => u.Id == _id).Project().To<ApplicationUser>().FirstOrDefault());
            }
        }

        private string _id
        {
            get
            {
                string id = string.Empty;
                //UserPrincipal u = UserPrincipal.Current;
                if (_identity.Name != string.Empty)
                {
                    using (UserPrincipal user = UserPrincipal.FindByIdentity(_contextUser, _identity.Name))
                    {
                        if (user != null)
                        {
                            id = user.Name;
                        }
                    }
                }
                return id;
            }
        }

        //public bool IsUserInRole(string role)
        //{
        //    var user = _context.Users.SingleOrDefault(u => u.Id == _id);
        //    var prin = new GenericPrincipal(_identity, user?.Roles.Distinct().Select(r=>r.Name).ToArray());
        //    HttpContext.Current.User = prin;
        //    //if (user == null) return false;
        //    //return user.Roles != null && user.Roles.Select(
        //    //     u => u.Name == role).Any();
        //    return HttpContext.Current.User.IsInRole(role);
        //}
    }
}