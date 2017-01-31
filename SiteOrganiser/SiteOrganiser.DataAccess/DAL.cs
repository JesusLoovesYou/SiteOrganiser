using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Toolkit.Web.Extenders;

namespace SiteOrganiser.DataAccess
{
    public class DAL
    {
        private SqlDatabase _sqlServerDB;

        /// <summary>
        /// Config dosyasındaki DB bağlantı bilgisini kullanarak MS Sql veritabanına bağlanır.
        /// </summary>
        internal DAL()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            _sqlServerDB = factory.Create("YurtBul") as SqlDatabase;
        }
        /// <summary>
        /// Config dosyasında baglanti parametresiyle belirtilmiş olan bağlantı bilgisini kullanarak MS Sql veritabanına bağlanır.
        /// </summary>
        /// <param name="connection"></param>
        internal DAL(string connection)
        {
            _sqlServerDB = new SqlDatabase(connection);
        }
        /// <summary>
        /// Verilen veritabanı nesnesi aracılığıyla bağlantıyı oluşturur.
        /// </summary>
        /// <param name="database"></param>
        internal DAL(Database database)
        {
            _sqlServerDB = database as SqlDatabase;
        }
        /// <summary>
        /// INSERT, UPDATE ve DELETE işlemleri için kullanılır.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        protected object ExecuteQuery(string query, List<DbParameter> parameters)
        {
            using (DbCommand queryCmd = SqlServerDB.GetSqlStringCommand(query))
            {
                addInParameters(parameters, queryCmd);
                return SqlServerDB.ExecuteScalar(queryCmd);
            }
        }
        /// <summary>
        /// StoredProcedure işlemleri için kullanılır.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        protected object ExecuteByStoredProcedure(string spName, List<DbParameter> parameters)
        {
            using (DbCommand queryCmd = SqlServerDB.GetStoredProcCommand(spName))
            {
                addInParameters(parameters, queryCmd);
                return SqlServerDB.ExecuteDataSet(queryCmd);
            }
        }

        private void addInParameters(List<DbParameter> parameters, DbCommand queryCmd)
        {
            foreach (DbParameter parameter in parameters)
            {
                SqlServerDB.AddInParameter(queryCmd, parameter.ParameterName, parameter.DbType, parameter.Value);
            }
        }

        protected List<T> GetListByStoredProc<T>(string storedProcName) where T : new()
        {
            using (DbCommand sprocCmd = SqlServerDB.GetStoredProcCommand(storedProcName))
            {
                DataSet ds = SqlServerDB.ExecuteDataSet(sprocCmd);
                var rows = ds.Tables[0].DataTableToObjectList<T>();
                return rows;
            }
        }

        protected List<T> GetListByQuery<T>(string query) where T : new()
        {
            using (DbCommand queryCmd = SqlServerDB.GetSqlStringCommand(query))
            {
                DataSet ds = SqlServerDB.ExecuteDataSet(queryCmd);
                var rows = ds.Tables[0].DataTableToObjectList<T>();
                return rows;
            }
        }

        protected List<T> GetListByQuery<T>(string query, List<DbParameter> parameters) where T : new()
        {
            using (DbCommand queryCmd = SqlServerDB.GetSqlStringCommand(query))
            {
                addInParameters(parameters, queryCmd);
                DataSet ds = SqlServerDB.ExecuteDataSet(queryCmd);
                var rows = ds.Tables[0].DataTableToObjectList<T>();
                return rows;
            }
        }

        internal SqlDatabase SqlServerDB
        {
            get { return _sqlServerDB; }
        }
    }
}
