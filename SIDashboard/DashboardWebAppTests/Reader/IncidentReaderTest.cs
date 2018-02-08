using Dashboard.DashboardDataReader;
using Dashboard.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class IncidentReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void IncidentReaderRetunsWithoutException()
        {
            var reader = new CustomerIncidentDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void IncidentResolveTimeReaderRetunsWithoutException()
        {
            var reader = new CustomerIncidentResolveTimeDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void IncidentCloseTimeReaderRetunsWithoutException()
        {
            var reader = new CustomerIncidentCloseTimeDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void SevAReaderRetunsWithoutException()
        {
            var reader = new SevADBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
