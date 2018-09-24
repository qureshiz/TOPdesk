using NUnit.Framework;
using System;
using TOPdesk.Context;
namespace TOPdesk.Tests.StoredProcedureTests
{
    [TestFixture]
    public class DailyCallsBreached
    {
        [Test]
        public void UTVF_DailyCallsBreached()
        {
            string startDateString  = "01 jan 2018";
            string endDateString    = "18 sep 2018";
            string region           = "Central";

            DateTime.TryParse(startDateString, out var startDate);
            DateTime.TryParse(endDateString, out var endDate);
            TopDesk577Entities db = new TopDesk577Entities();

            var results = db.UTVF_DailyCallsBreached(startDate, endDate, region);

            Assert.IsNotNull(results, "UTVF_DailyCallsBreached returned no results");
        }

        [Test]
        public void UTVF_DailyCallsBreached_Is_Breached()
        {
            string startDateString = "01 jan 2018";
            string endDateString = "18 sep 2018";
            string region = "Central";

            DateTime.TryParse(startDateString, out var startDate);
            DateTime.TryParse(endDateString, out var endDate);

            TopDesk577Entities db = new TopDesk577Entities();

            var results = db.UTVF_DailyCallsBreached(startDate, endDate, region);

            
                              
                              


        }
    }
}
