using System.Data.SqlClient;
using System.Data.Common;

namespace EntityModel.Connection
{
    public static class SQLConnection
    {
        public static SqlConnection GetSQLConnection(string serverName) //TOPdesk577 or TOPdeskLive.
        {
            //i.e. TOPdesk577, TOPdeskLive.
            var connectionString = IncidentConfigurationManager.GetConnectionString(serverName);
            var sqlConnection = new SqlConnection(connectionString);

            return sqlConnection;
        }

    }

}

