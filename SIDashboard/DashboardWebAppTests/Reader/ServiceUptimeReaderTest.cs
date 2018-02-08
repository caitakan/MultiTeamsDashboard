using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.DashboardDataReader;
using Dashboard.Shared;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class ServiceUptimeReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void TestServiceUptimeReaderRetunsWithoutException()
        {
            var reader = new ServiceUpTimeKustoDashboardDataReader(SalesIntelligenceWebApiConstant.QUEUE_SCORING_SERVICE_UPTIME,
                                                SalesIntelligenceWebApiConstant.INSIDE_SALES_KUSTO_KEY,
                                                SalesIntelligenceWebApiConstant.INSIDE_SALES_KUSTO_ENDPOINT);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
