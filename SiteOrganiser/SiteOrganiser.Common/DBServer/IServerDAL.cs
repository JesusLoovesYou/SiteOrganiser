using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Common.DBServer
{
    public interface IServerDAL
    {
        string CurrentConnectionString { get; }
        object Database { get; }
        IServerInfo GetServerInfo();
        IBLMessage SaveServerInfo(string serverUserName, string serverPassword, string serverName, string databaseName, int databaseType, string tempFolder);
        IBLMessage TestServerConnection();
        IBLMessage TestServerConnection(string tempFolder, string databaseName, string serverName, string serverUserName, string password);
        bool CheckDBFile();
        string GetCurrentConnectionString();
    }
}
