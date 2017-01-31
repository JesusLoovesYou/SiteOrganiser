namespace SiteOrganiser.Common.DBServer
{
    public interface IServerInfo
    {
        string ServerUserName { get; set; }
        string ServerPassword { get; set; }
        string ServerName { get; set; }
        string DatabaseName { get; set; }
        int DatabaseType { get; set; }
    }
}
