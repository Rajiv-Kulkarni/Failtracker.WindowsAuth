using System;
using AutoMapper;
using Heroic.AutoMapper;
using ISAT.Admin.Test.Web.Domain;

namespace ISAT.Admin.Test.Web.Models.Issue
{
    public class IssueSummaryViewModel : IHaveCustomMappings//IMapFrom<Domain.Issue>
    {
        public int IssueID { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorUserName { get; set; }
        public IssueType IssueType { get; set; }
        public string AssignedToUserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Domain.Issue, IssueSummaryViewModel>()
                .ForMember(m => m.AssignedToUserName, opt =>
                    opt.MapFrom(u => u.AssignedToUser.UserName))
                .ForMember(m => m.CreatorUserName, opt =>
                    opt.MapFrom(u => u.CreatedUser.UserName));
        }

    }
}