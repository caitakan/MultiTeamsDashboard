using Dashboard.DashboardDataReader;
using Dashboard.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class TestRunReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void TestRunReaderRetunsWithoutException()
        {
            var reader = new TestCoverageDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
