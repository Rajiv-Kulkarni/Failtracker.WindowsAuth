using System.ComponentModel.DataAnnotations;
using Heroic.AutoMapper;
using ISAT.Admin.Test.Web.Domain;

namespace ISAT.Admin.Test.Web.Models.Issue
{
    public class NewIssueForm : IHaveCustomMappings
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public IssueType IssueType { get; set; }

        [Required, Display(Name = "Assigned To")]
        public string AssignedToUserName { get; set; }

        [Required]
        public string Body { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Domain.Issue, EditIssueForm>()
                .ForMember(m => m.AssignedToUserName, opt =>
                    opt.MapFrom(u => u.AssignedToUser.UserName));
        }

    }
}