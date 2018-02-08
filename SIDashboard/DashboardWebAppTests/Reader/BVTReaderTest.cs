using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.DashboardDataReader;
using Dashboard.Shared;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class BVTReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void TestBVTReaderRetunsWithoutException()
        {
            var reader = new BVTDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
