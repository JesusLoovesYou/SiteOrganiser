using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.DAL;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.UserManagement;
using SiteOrganiser.DataAccess.DataEntities;
using SiteOrganiser.DataAccess.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Toolkit.Web.Extenders;

namespace SiteOrganiser.DataAccess
{
    public class UserManagementDAL : DAL, IUserManagementDAL
    {
        public UserManagementDAL() : base() { }

        public UserManagementDAL(string connection) : base(connection) { }

        public UserManagementDAL(Database database) : base(database) { }

        #region Registration
        public IBLMessage SaveApplication(string userEmail, string userPassword)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail },
                                                new SqlParameter("UserPassword", SqlDbType.VarChar) { Value = userPassword },
                                                new SqlParameter("VerifyGuid", SqlDbType.VarChar) { Value = Guid.NewGuid().ToString() }
                                            });
                return new DBMessage { Message = "Kayıt alındı.", Result = SaveAttempts.Success };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                return new DBMessage { Message = exception.Message, Result = SaveAttempts.Failure };
            }
        }

        public IBLMessage DeleteApplication(string userEmail)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;2", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail }
                                            });
                return new DBMessage { Message = "Başvuru silindi.", Result = SaveAttempts.Success };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                return new DBMessage { Message = "Başvuru silinmesi sırasında hata oluştu. Lütfen site yetkilisiyle görüşünüz.", Result = SaveAttempts.Failure };
            }
        }

        public void DeleteCustomer(string userEmail)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Customers;3", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail }
                                            });
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
            }
        }

        public IUserApplication GetApplicationByEmail(string userEmail)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;3", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail }
                                            });
                var ds = result as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    var list = ds.Tables[0].DataTableToObjectList<UserApplication>();
                    var app = list.FirstOrDefault();
                    return app;
                }
                return null;
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                throw;
            }
        }

        public IUserApplication GetApplicationByVerifyGuid(string verifyGuid)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;4", new List<DbParameter>
                                            {
                                                new SqlParameter("VerifyGuid", SqlDbType.VarChar) { Value = verifyGuid }
                                            });
                var ds = result as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    var list = ds.Tables[0].DataTableToObjectList<UserApplication>();
                    var app = list.FirstOrDefault();
                    return app;
                }
                return null;
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                throw;
            }
        }

        public void SetCustomerInfo(string userEmail)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Customers;2", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail }
                                            });
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                throw;
            }
        }

        public IBLMessage SaveResetRequest(string userEmail)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;5", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail },
                                                new SqlParameter("ResetGuid", SqlDbType.VarChar) { Value = Guid.NewGuid().ToString() }
                                            });
                return new DBMessage { Message = "Yenileme isteği alındı.", Result = SaveAttempts.Success };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                return new DBMessage { Message = exception.Message, Result = SaveAttempts.Failure };
            }
        }

        public IResetRequest GetResetRequestByEmail(string userEmail)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;6", new List<DbParameter>
                                            {
                                                new SqlParameter("UserEmail", SqlDbType.VarChar) { Value = userEmail }
                                            });
                var ds = result as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    var list = ds.Tables[0].DataTableToObjectList<ResetRequest>();
                    var req = list.FirstOrDefault();
                    return req;
                }
                return null;
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                throw;
            }
        }

        public IResetRequest GetResetRequestByResetGuid(string resetGuid)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;7", new List<DbParameter>
                                            {
                                                new SqlParameter("ResetGuid", SqlDbType.VarChar) { Value = resetGuid }
                                            });
                var ds = result as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    var list = ds.Tables[0].DataTableToObjectList<ResetRequest>();
                    var req = list.FirstOrDefault();
                    return req;
                }
                return null;
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                throw;
            }
        }

        public IBLMessage DeleteResetRequest(string resetGuid)
        {
            try
            {
                var result = ExecuteByStoredProcedure("sp_IUS_Registration;8", new List<DbParameter>
                                            {
                                                new SqlParameter("ResetGuid", SqlDbType.VarChar) { Value = resetGuid }
                                            });
                return new DBMessage { Message = "Şifre yenilendi.", Result = SaveAttempts.Success };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementDAL");
                return new DBMessage { Message = "Şifre yenilenmesi sırasında hata oluştu. Lütfen site yetkilisiyle görüşünüz.", Result = SaveAttempts.Failure };
            }
        }

        #endregion
    }
}
