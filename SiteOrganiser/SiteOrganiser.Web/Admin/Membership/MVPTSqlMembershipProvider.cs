using System.Collections.Specialized;
using System.Reflection;
using System.Web.Profile;
using System.Web.Security;
using SiteOrganiser.Presentation;

namespace SiteOrganiser.Web.Admin.Membership
{
    public static class MVPTConnection
    {
        public static string BuildMyCustomConnectionString()
        {
            string connectionString = SitePresenter.GetCurrentConnectionString();
            return connectionString; //@"Data Source=MYPC\SQLE2012;Initial Catalog=YurtBul;Persist Security Info=True;User ID=yurt;Password=qwe123";
        }
    }

    public class MVPTSqlMembershipProvider : SqlMembershipProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            string connectionString = config["connectionString"] = MVPTConnection.BuildMyCustomConnectionString();
            base.Initialize(name, config);
            // Set private property of Membership provider.
            FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }

    }
    public class MVPTProfileProvider : SqlProfileProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            string connectionString = config["connectionString"] = MVPTConnection.BuildMyCustomConnectionString();
            base.Initialize(name, config);// Update the private connection string field in the base class.
            // Set private property of Membership provider.
            var connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }
    }
    public class MVPTRoleProvider : SqlRoleProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            string connectionString = config["connectionString"] = MVPTConnection.BuildMyCustomConnectionString();
            base.Initialize(name, config);// Update the private connection string field in the base class.
            // Set private property of Membership provider.
            var connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }
    }
}