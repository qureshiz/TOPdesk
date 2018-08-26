using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOPdesk._01_EntiryModel;

namespace TOPdesk.Tests.StoredProcedureTests
{
    [TestClass]
    public class USP_Incident_Tests
    {
        private object incidentList;

        [TestMethod]
        public void StoredProcedureTest01 ()
        {
            var operatorIDString = "08921908-EBA0-4A20-B2C7-703A05569338";
            var startDateString = "02 jul 2018";
            var endDateString = "03 jul 2018";

            DateTime.TryParse(startDateString, out var startDate);
            DateTime.TryParse(endDateString, out var endDate);
            Guid.TryParse(operatorIDString, out var operatorId);

            TopDesk577Entities db = new TopDesk577Entities();
            var results = db.UTVF_IncidentsMovedToOperator(operatorId, startDate, endDate);

            Assert.IsNotNull(results, "Stored Proceducre returned no results");
            Assert.IsTrue(results.Any(), "Doesn't have any records");

            var incident = results.FirstOrDefault();
            Assert.IsNotNull(incident, "Incident is null");
            Assert.IsFalse(string.IsNullOrEmpty(incident.Incident), "Incident text is empty");
        }

        [TestMethod]
        public void StoredProcedureTest02()
        {
            var operatorIDString = "08921908-EBA0-4A20-B2C7-703A05569338";
            var startDateString = "02 jul 2018";
            var endDateString = "03 jul 2018";

            DateTime.TryParse(startDateString, out var startDate);
            DateTime.TryParse(endDateString, out var endDate);
            Guid.TryParse(operatorIDString, out var operatorId);

            TopDesk577Entities db = new TopDesk577Entities();
            var results = db.UTVF_IncidentsMovedToOperator(operatorId, startDate, endDate);
            Assert.IsNotNull(results, "Stored Proceducre returned no results");
            Assert.IsTrue(results.Any(), "Doesn't have any records");

            var incident = results.FirstOrDefault();  // Incident number.
            Assert.IsNotNull(incident, "Incident is null");
            Assert.IsFalse(string.IsNullOrEmpty(incident.Incident), "Incident text is empty");

            incident incidentItem = db.incidents.Where(x => x.naam == incident.Incident).FirstOrDefault();
            Assert.IsNotNull(incidentItem, "Stored Proceducre returned no results");
            Assert.IsTrue(incidentItem.naam == incident.Incident, "Incident Records don't match");

            var results2 = db.UTVF_IncidentOperatorMovements(incident.Incident);

            Assert.IsNotNull(results2, "Stored Proceducre returned no results");
            Assert.IsTrue(results2.Any(), "Doesn't have any records");

            var incident2 = results2.FirstOrDefault();
            Assert.IsNotNull(incident.Incident, "Incident is null");
            Assert.IsFalse(string.IsNullOrEmpty(incident.Incident), "Incident text is empty");

            var currentDate = new DateTime();
            results2.ToList().ForEach(item =>
            {
                Assert.IsTrue(item.Move_Date != null, "Move date is null");
                Assert.IsTrue(item.Move_Date > currentDate, "Move Date is not is sequencial order :" + currentDate.ToString() + " " + item.Move_Date.ToString() + " Row " + item.RowNumber);
                currentDate = (DateTime)item.Move_Date;
            });

            JSON_FileExport.WriteFile("StoredProcedureTest02", results2, results2.Count(), "USP_Functions");
        }

        [TestMethod]
        public void Call_SQL_Stored_Procedure_USP_GetIncidentsOperatorOperatorGroupMovements()
        {
            //int returnValue = 0; // Return value of Stored Procedure.
            int operatorOrOperatorGroup = 1; // 1 Operator, 2 Operator Group.
            string operatorIDString = "08921908-EBA0-4A20-B2C7-703A05569338"; // Zishan Qureshi Operator ID.
            var startDateString = "02 jul 2018";
            var endDateString = "03 jul 2018";

            DateTime.TryParse(startDateString, out var startDate);
            DateTime.TryParse(endDateString, out var endDate);
            Guid.TryParse(operatorIDString, out var operatorId);

            TopDesk577Entities db = new TopDesk577Entities();
            var results = db.USP_GetIncidentsOperatorOperatorGroupMovements(operatorOrOperatorGroup, operatorId, null, startDate, endDate);
            Assert.IsNotNull(results, "Stored Proceducre returned no results");
            //Assert.IsTrue(results.Any(), "Doesn't have any records");  //  'int' does not contain a definition for 'Any' and no extension method 'Any' accepting a first argument of ...

            // Iterate the results variable and check that the ChangedTo field has a value.
            
        }

    }
}
