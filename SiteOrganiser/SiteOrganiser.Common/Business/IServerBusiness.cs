using SiteOrganiser.Common.DBServer;
using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Common.Business
{
    public interface IServerBusiness
    {
        string CurrentConnectionString { get; }
        object Database { get; }
        IBLMessage TestServerConnection();
        IBLMessage TestServerConnection(string tempFolder, string databaseName, string serverName, string serverUserName, string password);
        bool CheckDBFile();
        IServerInfo GetServerInfo();
        IBLMessage SaveServerInfo(string serverUserName, string serverPassword, string serverName, string databaseName, int databaseType, string tempFolder);
        string GetCurrentConnectionString();
    }
}
