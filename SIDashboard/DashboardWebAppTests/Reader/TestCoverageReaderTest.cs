using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.DashboardDataReader;
using Dashboard.Shared;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class TestCoverageReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void TestCoverageReaderRetunsWithoutException()
        {
            var reader = new TestCoverageDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
