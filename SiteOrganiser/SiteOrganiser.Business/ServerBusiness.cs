using DependencyResolver;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.DBServer;
using SiteOrganiser.Common.Messages;
using Ninject;

namespace SiteOrganiser.Business
{
    public class ServerBusiness : IServerBusiness
    {
        private IServerDAL ServerDAL { get; set; }

        public ServerBusiness()
        {
            IKernel kernel = NinjectDependencyResolver.CurrentKernel;
            ServerDAL = kernel.Get<IServerDAL>();
        }
        public IBLMessage TestServerConnection()
        {
            return ServerDAL.TestServerConnection();
        }

        public IBLMessage TestServerConnection(string tempFolder, string databaseName, string serverName, string serverUserName, string password)
        {
            return ServerDAL.TestServerConnection(tempFolder, databaseName, serverName, serverUserName, password);
        }

        public bool CheckDBFile()
        {
            return ServerDAL.CheckDBFile();
        }

        public IServerInfo GetServerInfo()
        {
            return ServerDAL.GetServerInfo();
        }

        public IBLMessage SaveServerInfo(string serverUserName, string serverPassword, string serverName, string databaseName, int databaseType, string tempFolder)
        {
            return ServerDAL.SaveServerInfo(serverUserName, serverPassword, serverName, databaseName, databaseType, tempFolder);
        }

        public string GetCurrentConnectionString()
        {
            return ServerDAL.GetCurrentConnectionString();
        }

        public string CurrentConnectionString
        {
            get
            {
                return ServerDAL.CurrentConnectionString;
            }
        }

        public object Database
        {
            get { return ServerDAL.Database; }
        }
    }
}
