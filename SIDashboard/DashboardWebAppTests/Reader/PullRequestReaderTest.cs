using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.DashboardDataReader;
using Dashboard.Shared;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class PullRequestReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void TestPRReaderRetunsWithoutException()
        {
            var reader = new PullRequestDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
