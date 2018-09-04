using System.Data.SqlClient;
using EntityModel;
using System.Configuration;

namespace TOPdesk.Service
{
    public abstract class BaseService
    {
        //class SQLConnection
        //{
        //    public SqlConnection GetSQLConnection(string serverName) //TOPdesk577 or TOPdeskLive.
        //    {
        //        var sqlConnectionString = new IncidentConfigurationManager();   // Returns the SQL Connection string to the SQL Server 
        //                                                                        //i.e. TOPdesk577, TOPdeskLive.
        //        var connectionString = sqlConnectionString.GetConnectionString(serverName);
        //        SqlConnection sqlConnection = new SqlConnection(connectionString);

        //        return sqlConnection;
        //    }

        //}

    }

    public class IncidentConfigurationManager
    {
        public ConnectionStringSettingsCollection GetConfigManager()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;

            return connectionStrings;
        }

        public string GetConnectionString(string connectionStringName)
        {
            var returnValue = "";

            var connectionStrings = GetConfigManager()[connectionStringName];
            returnValue = connectionStrings.ConnectionString;

            return returnValue;
        }
    }
}