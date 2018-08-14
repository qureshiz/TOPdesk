using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityModel.Tests
{
    [TestClass]
    public class IncidentBreachedTests
    {
        [TestMethod]
        public void Can_Call_TOPDesk577_SQL_StoredProcedure_USP_Get_Incident_Is_Breached()
        {
            // Assign
            var connectionStringName = "TOPdesk577";
            var configManager = new IncidentConfigurationManager();
            var connectionString = configManager.GetConnectionString(connectionStringName);
            var storeProcedureName = "USP_Get_Incident_Is_Breached";
            var incident = new Incident()
            {
                IncidentNumber = "I1805-0834"
            };

            var expectedResult = 1;

            // Act
            // Assert
            using (var sqlConnection = new SqlConnection(connectionString))
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
                returnValueParam.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValueParam);

                cmd.ExecuteNonQuery();
               
                int returnValue = (int)cmd.Parameters["@breachedIncident"].Value;  //(int)outPutParameter.Value;
                int.TryParse(cmd.Parameters["@breachedIncident"].Value.ToString(), out int returnValue2);  

                Assert.IsNotNull(returnValue, "Return value is null");
                Assert.IsTrue(returnValue == expectedResult, "Return value is not as expected " + returnValue.ToString());

                sqlConnection.Close();
            }
        }

    }

}
