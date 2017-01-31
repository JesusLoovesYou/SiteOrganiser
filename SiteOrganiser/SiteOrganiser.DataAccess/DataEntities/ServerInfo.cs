using SiteOrganiser.Common.DBServer;

namespace SiteOrganiser.DataAccess.DataEntities
{
    public class ServerInfo : IServerInfo
    {
        public string ServerUserName { get; set; }
        public string ServerPassword { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public int DatabaseType { get; set; }
    }
}
