using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.DashboardDataReader;
using Dashboard.Shared;

namespace DashboardWebAppTests.Reader
{
    [TestClass]
    public class OfficalBuildReaderTest
    {
        private const string DBConnection = SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString;

        [TestMethod]
        public void TestOfficialBuildReaderRetunsWithoutException()
        {
            var reader = new OfficialBuildDBDashboardDataReader(DBConnection);
            var data = reader.read();
            Assert.IsNotNull(data);
        }
    }
}
