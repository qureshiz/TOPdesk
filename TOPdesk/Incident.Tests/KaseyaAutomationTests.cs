using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOPdesk.Service;
using System.Data.SqlClient;
namespace EntityModel.Tests
{
    [TestClass]
    public class KaseyaAutomationTests
    {
        //    [TestMethod]
        //    public void Can_Call_TOPDesk577_SQL_StoredProcedure_USP_Get_SubjectFromIncident()
        //    {
        //        // Assign.
        //        var sqlInstanceName = "TOPdesk577";
        //        var configManager = new IncidentConfigurationManager();
        //        string connectionString = configManager.GetConnectionString(sqlInstanceName);
        //        string storedProcedureName = "USP_Get_SubjectFromIncident";
        //        string expectedSubject = "Subject: Monitoring generated Counter ALARM at 8:24:25 am 9-Jul-18 on pcpri-tml01.reading.paperchase N/A";
        //        string incidentRequest = "";

        //        // Example Incident?
        //        var incident = new Incident;
        //        incident.IncidentNumber = "I1706-14426";

        //        // Action
        //        // Assert
        //    }

        public void Can_Get_Request_From_Incident()
        {
            // Request comes from the incident.verzoek field.
            string sqlInstanceName = "TOPdesk577";
            var configManager = new IncidentConfigurationManager();
            // The IncidentConfigurationManager returns an SQL Connection string for the specified SQL Instance.
            string connectionString = configManager.GetConnectionString(sqlInstanceName);
            string request = "";
            string sqlStatement = "SELECT i.verzoek from incident where i.naam = @incidentNumber";

            var incident = new Incident();
            incident.IncidentNumber = "I1706-14426";
            
        }

    }
}
