using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.DAL;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.System;
using SiteOrganiser.DataAccess.DataEntities;
using Toolkit.Web.Extenders;

namespace SiteOrganiser.DataAccess
{
    public class UserDAL : DAL, IUserDAL
    {
        public UserDAL() : base() { }

        public UserDAL(string connection) : base(connection) { }

        public UserDAL(Database database) : base(database) { }

        #region Implementation of IUserDAL
        public ICredential GetUserCredentials(string userName)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Users", new List<DbParameter>
                                            {
                                                new SqlParameter("Username", SqlDbType.VarChar) { Value = userName }
                                            });
                var ds = result as DataSet;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var users = ds.Tables[0].DataTableToObjectList<User>();
                    var user = users.FirstOrDefault();
                    user.UserRoles = GetUserRoles(user.UserId.ToString());
                    return new Credential(user.UserId, user.UserName, user.UserRoles, true);
                }
                return new Credential(Guid.Empty, "", new List<IRole>(), false);
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserDAL");
                throw;
            }
        }

        public List<IRole> GetUserRoles(string userId)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Roles", new List<DbParameter>
                                            {
                                                new SqlParameter("UserId", SqlDbType.VarChar) { Value = userId }
                                            });
                var ds = result as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    var roles = ds.Tables[0].DataTableToObjectList<Role>();
                    return new List<IRole>(roles);
                }
                return new List<IRole>();
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserDAL");
                throw;
            }
        }

        #endregion
    }
}
