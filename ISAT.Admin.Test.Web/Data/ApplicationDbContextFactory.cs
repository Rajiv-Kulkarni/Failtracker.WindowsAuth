using System.Data.Entity.Infrastructure;

namespace ISAT.Admin.Test.Web.Data
{
    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext(ApplicationDbContext.conStr);
        }
    }
}