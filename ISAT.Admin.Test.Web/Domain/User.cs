namespace ISAT.Admin.Test.Web.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            AssignedIssues = new HashSet<Issue>();
            CreatedIssues = new HashSet<Issue>();
            Logs = new HashSet<LogAction>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            Roles = new HashSet<Role>();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        [Required]
        [StringLength(128)]
        public string Discriminator { get; set; }

        public virtual ICollection<Issue> AssignedIssues { get; set; }

        public virtual ICollection<Issue> CreatedIssues { get; set; }

        public virtual ICollection<LogAction> Logs { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
