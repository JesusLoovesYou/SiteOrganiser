using DependencyResolver;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Presentation
{
    public static class SitePresenter
    {
        private static IServerBusiness bl;

        static SitePresenter()
        {
            bl = NinjectDependencyResolver.GetBL<IServerBusiness>();
        }

        public static IBLMessage TestServerConnection()
        {
            return bl.TestServerConnection();
        }

        public static bool CheckDBFile()
        {
            return bl.CheckDBFile();
        }

        public static string GetCurrentConnectionString()
        {
            return bl.GetCurrentConnectionString();
        }
    }
}
