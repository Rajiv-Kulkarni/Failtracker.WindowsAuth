using System;
using System.Linq;
using System.Web.Security;
using ISAT.Admin.Test.Web.Data;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    //MattQuestion: How to inject AppDbContext here? Is this class needed at all?
    public class CustomRoleProvider : RoleProvider
    {
        public ApplicationDbContext _context { get; set; } 

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == username);
            if (user == null)
                return false;
            return user.Roles != null && user.Roles.Select(r => r.Name == roleName).Any();
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == username);
                if (user == null)
                    return new string[] { };
                return user.Roles?.Select(r => r.Name).ToArray() ?? new string[] { };
            }
            catch (Exception)
            {
                return new string[] { };
                throw;
            }
        }

        public override string[] GetAllRoles()
        {
            return _context.Roles.Select(r => r.Name).ToArray();
        }

        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}