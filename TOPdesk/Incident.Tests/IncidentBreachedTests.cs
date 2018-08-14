using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EntityModel.Tests
{

    public class IncidentBreachedTests
    {
        public void Can_Call_TOPDesk577_SQL_StoredProcedure_USP_Get_Incident_Is_Breached()
        {
            // Assign
            // Act
            // Assert
            var connectionStringName = "TOPdesk577";
            var configManager = new IncidentConfigurationManager();
            var connectionString = configManager.GetConnectionString(connectionStringName);
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            
            
            var command = new SqlCommand("USP_Get_Incident_Is_Breached", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter = command.Parameters.Add("@is_Breached", SqlDbType.Int);

        }

    }
}
