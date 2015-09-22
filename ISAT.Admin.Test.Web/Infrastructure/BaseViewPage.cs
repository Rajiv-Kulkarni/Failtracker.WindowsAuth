using System.Web.Mvc;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public abstract class BaseViewPage : WebViewPage
    {
        public ICurrentUser _CurrentUser { get; set; } //need to be able to inject this using property injection

        public ICurrentUser ViewCurrentUser 
        {
            get
            {
                return _CurrentUser;
            }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public ICurrentUser _CurrentUser { get; set; }

        public ICurrentUser ViewCurrentUser
        {
            get
            {
                return _CurrentUser;
            }
        }
    }

}