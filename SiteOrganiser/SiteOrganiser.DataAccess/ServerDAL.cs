using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.DBServer;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.DataAccess.DataEntities;
using SiteOrganiser.DataAccess.Helpers;
using SiteOrganiser.DataAccess.Messages;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SiteOrganiser.DataAccess
{
    public class ServerDAL : IServerDAL
    {
        private string _currentConnectionString;
        private string _databaseName;
        private object _database;
        private const string ConnectionStringFormat = @"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

        private string ServerFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Admin\\" + "ServerDB.db";

        public ServerDAL()
        {
            var serverInfo = GetServerInfo();
            if (serverInfo == null) _currentConnectionString = string.Empty;
            else
            {
                _currentConnectionString = String.Format(ConnectionStringFormat,
                                                         serverInfo.ServerName,
                                                         serverInfo.DatabaseName,
                                                         serverInfo.ServerUserName,
                                                         serverInfo.ServerPassword);
                _databaseName = serverInfo.DatabaseName;
                _database = String.IsNullOrEmpty(_currentConnectionString) ? null : new SqlDatabase(_currentConnectionString);
            }
        }

        private String[] GetFromFile(string dosyaIsmi)
        {
            if (File.Exists(dosyaIsmi))
            {
                using (var dosyaOku = File.OpenText(dosyaIsmi))
                {
                    var icerik = dosyaOku.ReadLine();
                    if (!String.IsNullOrEmpty(icerik)) return icerik.Split(',').Select(Encryption.Decrypt).ToArray();
                }
            }

            return new string[0];
        }

        private bool testServer(string connectionString, string veritabaniAdi)
        {
            string commandText = String.Format("select TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM [{0}].INFORMATION_SCHEMA.TABLES;", veritabaniAdi);
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                // Call Read before accessing data.
                while (reader.Read())
                {
                    // eger buraya kadar hata gelmediyse demekki veritabani yerinde.
                    var row = reader[0];
                    if (row.ToString().Equals(veritabaniAdi))
                    {
                        return true;
                    }
                }

                // Call Close when done reading.
                reader.Close();
            }
            return false;
        }

        #region Implementation of IServerDAL

        public string CurrentConnectionString
        {
            get { return _currentConnectionString; }
        }

        public object Database
        {
            get
            {
                return _database;
            }
        }

        public IServerInfo GetServerInfo()
        {
            var parameters = GetFromFile(ServerFileName);
            return parameters.Any()
                        ? new ServerInfo
                        {
                            ServerUserName = parameters[0],
                            ServerPassword = parameters[1],
                            ServerName = parameters[2],
                            DatabaseName = parameters[3],
                            DatabaseType = 1
                        }
                        : null;
        }

        public IBLMessage SaveServerInfo(string serverUserName, string serverPassword, string serverName, string databaseName, int databaseType, string tempFolder)
        {
            try
            {
                using (var dosya = new StreamWriter(String.IsNullOrEmpty(tempFolder) ? ServerFileName : tempFolder))
                {
                    dosya.WriteLine("{0},{1},{2},{3}", Encryption.Encrypt(serverUserName), Encryption.Encrypt(serverPassword), Encryption.Encrypt(serverName), Encryption.Encrypt(databaseName));
                    dosya.Close();
                }
                return new DBMessage { Message = "Sunucu bilgileri güncellendi.", Result = SaveAttempts.Success };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "ServerDAL");
                return new DBMessage { Message = "Kayıt esnasında bir hata oluştu.", Result = SaveAttempts.Failure };
            }
        }

        public IBLMessage TestServerConnection()
        {
            try
            {
                var result = testServer(_currentConnectionString, _databaseName);
                return result
                    ? new DBMessage { Message = "", Result = SaveAttempts.Success }
                    : new DBMessage { Message = "Sunucu bulunamadı. Bağlantı bilgilerini kontrol ediniz.", Result = SaveAttempts.Failure };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "ServerDAL");
                return new DBMessage { Message = "Sunucu bağlantısı esnasında hata oluştu. Bağlantı bilgilerini kontrol ediniz. Detaylı bilgi için log dosyalarını inceleyeniz.", Result = SaveAttempts.Failure };
            }
        }

        public IBLMessage TestServerConnection(string tempFolder, string databaseName, string serverName, string serverUserName, string password)
        {
            try
            {
                _currentConnectionString = String.Format(ConnectionStringFormat,
                                                         serverName,
                                                         databaseName,
                                                         serverUserName,
                                                         password);
                _databaseName = databaseName;
                var result = testServer(_currentConnectionString, _databaseName);
                return result
                    ? new DBMessage { Message = "", Result = SaveAttempts.Success }
                    : new DBMessage { Message = "Sunucu bulunamadı. Bağlantı bilgilerini kontrol ediniz.", Result = SaveAttempts.Failure };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "ServerDAL");
                return new DBMessage { Message = "Sunucu bağlantısı esnasında hata oluştu. Bağlantı bilgilerini kontrol ediniz. Detaylı bilgi için log dosyalarını inceleyeniz.", Result = SaveAttempts.Failure };
            }
        }

        public bool CheckDBFile()
        {
            if (File.Exists(ServerFileName))
            {
                using (var dosyaOku = File.OpenText(ServerFileName))
                {
                    var icerik = dosyaOku.ReadLine();
                    if (!String.IsNullOrEmpty(icerik)) return true;
                }
            }

            return false;
        }

        public string GetCurrentConnectionString()
        {
            return _currentConnectionString;
        }

        #endregion
    }
}
