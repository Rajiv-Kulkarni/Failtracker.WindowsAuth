using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Heroic.AutoMapper;
using ISAT.Admin.Test.Web.Domain;

namespace ISAT.Admin.Test.Web.Models.Issue
{
    public class EditIssueForm : IHaveCustomMappings//IMapFrom<ISAT.Admin.Test.Web.Domain.Issue>
    {
        [HiddenInput]
        public int IssueID { get; set; }

        [ReadOnly(true)]
        public string CreatorUserName { get; set; }

        [Required]
        public string Subject { get; set; }

        public IssueType IssueType { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedToUserName { get; set; }

        [Required]
        public string Body { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Domain.Issue, EditIssueForm>()
                .ForMember(m => m.AssignedToUserName, opt =>
                    opt.MapFrom(u => u.AssignedToUser.UserName))
                .ForMember(m => m.CreatorUserName, opt =>
                    opt.MapFrom(u => u.CreatedUser.UserName));
        }

    }
}