using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.Connection
{
    public static class IncidentConfigurationManager
    {
        public static ConnectionStringSettingsCollection GetConfigManager()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;

            return connectionStrings;
        }

        public static string GetConnectionString(string connectionStringName)
        {
            var returnValue = "";

            var connectionStrings = GetConfigManager()[connectionStringName];
            returnValue = connectionStrings.ConnectionString;

            return returnValue;
        }
    }
}
