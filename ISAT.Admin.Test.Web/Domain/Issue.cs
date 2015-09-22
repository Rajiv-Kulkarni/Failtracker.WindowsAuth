namespace ISAT.Admin.Test.Web.Domain
{
    using System;

    public partial class Issue
    {
        private User user11;
        private User user12;
        private IssueType bug;
        private string v1;
        private string v2;

        public int IssueID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public IssueType IssueType { get; set; }

        public string AssignedTo_Id { get; set; }

        public string Creator_Id { get; set; }

        public virtual User AssignedToUser { get; set; }

        public virtual User CreatedUser { get; set; }

        //For EF...
        public Issue()
        {

        }

        public Issue(ApplicationUser creator, ApplicationUser assignedTo, IssueType type, string subject, string body)
        {
            Creator_Id = creator.Id;
            AssignedTo_Id = assignedTo.Id;
            Subject = subject;
            Body = body;
            CreatedAt = DateTime.Now;
            IssueType = type;
        }

    }
}
