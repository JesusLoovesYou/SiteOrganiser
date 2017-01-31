using DependencyResolver;
using SiteOrganiser.Common.Business;

namespace SiteOrganiser.Presentation
{
    public class PresenterBase<T>
    {
        protected T bl;

        public PresenterBase()
        {
            var serverBusiness = NinjectDependencyResolver.GetBL<IServerBusiness>();
            if (serverBusiness != null)
            {
                bl = NinjectDependencyResolver.GetBL<T>(serverBusiness.Database);
            }
            else
            {
                bl = NinjectDependencyResolver.GetBL<T>();
            }
        }
    }
}
