using System.DirectoryServices.AccountManagement;
using ISAT.Admin.Test.Web.Data;
using StructureMap.Configuration.DSL;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public class DbContextRegistry : Registry
    {
        public DbContextRegistry()
        {
            For<ApplicationDbContext>().Use(() => new ApplicationDbContext(ApplicationDbContext.conStr));
            For<PrincipalContext>().Use(() => new PrincipalContext(ContextType.Domain));
        }
    }
}