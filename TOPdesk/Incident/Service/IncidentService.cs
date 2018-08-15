using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using System.Data.SqlClient;
using System.Data;

namespace TOPdesk.Service

{
    class IncidentService
    {
        public int IsIncidentBreached(string incidentNumber, string sqlInstance)
        {
            // Assign
            var connectionStringName = sqlInstance;
            var configManager = new IncidentConfigurationManager();
            var connectionString = configManager.GetConnectionString(connectionStringName);
            var storeProcedureName = "USP_Get_Incident_Is_Breached";

            var incident = new Incident()
            {
                IncidentNumber = incidentNumber
            };

            // Act
            // Assert

            using (var sqlConnection = new SqlConnection(connectionString))
            {

                try
                {

                    sqlConnection.Open();

                    var cmd = new SqlCommand(storeProcedureName, sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter();

                    param = cmd.Parameters.Add("@strIncident", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = incident.IncidentNumber;

                    //Add the output parameter to the command object
                    SqlParameter returnValueParam = new SqlParameter();
                    returnValueParam.ParameterName = "@breachedIncident";
                    returnValueParam.SqlDbType = SqlDbType.Int;

                    returnValueParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValueParam);
                    // Calling stored procedure with return value
                    // https://stackoverflow.com/questions/6210027/calling-stored-procedure-with-return-value
                    cmd.ExecuteNonQuery();

                    int returnValue = (int)cmd.Parameters["@breachedIncident"].Value;  //(int)outPutParameter.Value;
                    int.TryParse(cmd.Parameters["@breachedIncident"].Value.ToString(), out int returnValue2);
                   
                   return returnValue;

                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception", e);
                   
                }
                finally
                {
                    // Close the sql connection.
                    sqlConnection.Close();
                    
                }

            }


        }

    }
}
