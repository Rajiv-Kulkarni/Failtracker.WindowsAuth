using System;
using System.ComponentModel;
using Heroic.AutoMapper;
using ISAT.Admin.Test.Web.Domain;
using ISAT.Admin.Test.Web.Infrastructure.ModelMetadata;

namespace ISAT.Admin.Test.Web.Models.Issue
{
    public class IssueDetailsViewModel : IHaveCustomMappings//IMapFrom<Domain.Issue>
    {
        [Render(ShowForEdit = false)]
        public int IssueID { get; set; }

        [Render(ShowForEdit = false)]
        public DateTime CreatedAt { get; set; }

        [ReadOnly(true)]
        public string CreatorUserName { get; set; }

        public string Subject { get; set; }

        public IssueType IssueType { get; set; }

        public string AssignedToUserName { get; set; }

        public string Body { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Domain.Issue, IssueDetailsViewModel>()
                .ForMember(m => m.AssignedToUserName, opt =>
                    opt.MapFrom(u => u.AssignedToUser.UserName))
                .ForMember(m => m.CreatorUserName, opt =>
                    opt.MapFrom(u => u.CreatedUser.UserName));
        }

    }
}