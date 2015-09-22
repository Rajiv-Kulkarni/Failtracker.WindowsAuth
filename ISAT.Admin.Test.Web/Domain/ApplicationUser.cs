using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Heroic.AutoMapper;
using AutoMapper;

namespace ISAT.Admin.Test.Web.Domain
{
    public class ApplicationUser : IHaveCustomMappings//IMapFrom<User>
    {
        public string Id { get; set; }
        public string MyName { get; set; }
        public IList<Role> MyRoles { get; set; }
        public IList<Issue> Assignments { get; set; }
        //MattQuestion: Do I add additional properties for the user here?

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, ApplicationUser>()
                .ForMember(m => m.MyName, opt =>
                    opt.MapFrom(u => u.UserName))
                .ForMember(m => m.MyRoles, opt =>
                    opt.MapFrom(u => u.Roles))
                .ForMember(m => m.Assignments, opt =>
                    opt.MapFrom(u => u.AssignedIssues));
        }

    }

}