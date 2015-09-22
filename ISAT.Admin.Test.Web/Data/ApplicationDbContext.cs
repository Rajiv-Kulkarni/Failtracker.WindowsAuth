using System.Data.Entity;
using ISAT.Admin.Test.Web.Domain;

namespace ISAT.Admin.Test.Web.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext()
        //    : base("name=ApplicationDbContext")
        //{
        //}

        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<LogAction> Logs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.AssignedIssues)
                .WithRequired(e => e.AssignedToUser)
                .HasForeignKey(e => e.AssignedTo_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CreatedIssues)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.Creator_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Logs)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.PerformedBy_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserClaims)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_Id);
        }
    }
}
