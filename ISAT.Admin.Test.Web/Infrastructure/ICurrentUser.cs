using ISAT.Admin.Test.Web.Domain;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public interface ICurrentUser //: IPrincipal //MattQuestion: Can this interface be derived from IPrincipal, if yes, what will the CurrentUser class look like
    {
        ApplicationUser Me { get; }
        //bool IsUserInRole(string role);
    }
}
