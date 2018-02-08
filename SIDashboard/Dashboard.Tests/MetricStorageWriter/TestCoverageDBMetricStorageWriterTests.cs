using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using System.Collections.Generic;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Newtonsoft.Json;
using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common;

namespace Dashboard.Tests.MetricStorageWriter
{
    [TestClass]
    public class TestCoverageDBMetricStorageWriterUnitTest
    {
        private SolutionConfig config = null;

        [TestInitialize]
        public void InitializeWriterTestClass()
        {
            this.config = SolutionConfigManager.GetSolutionConfig(SolutionName.SalesIntelligence);
            this.config.OverrideDashboardDBConnection(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV].ToString());
        }

        [TestMethod]
        public void TestCoverage_Insert_Test()
        {
            var writer = new TestCoverageDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var detail = new Dictionary<string, object>();

            var buildId = 10000000;
            detail["VSO"] = "dltc";
            detail["Project"] = "myProj";
            detail["BuildId"] = buildId;
            detail["ModuleName"] = "abc";
            detail["LinesCovered"] = 100;
            detail["LinesNotCovered"] = 50;

            var m1 = new Metric("TestCoverage", MetricType.TestCoverage, detail);

            writer.Write(m1);
            var tc = DBUtil.GetTestCoverage(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString(), "dltc", "myProj", buildId);

            Assert.AreEqual(tc.VSO, "dltc");
            Assert.AreEqual(tc.Project, "myProj");
            Assert.AreEqual(tc.BuildId, buildId);
            Assert.AreEqual(tc.ModuleName, "abc");
            Assert.AreEqual(tc.LinesCovered, 100);
            Assert.AreEqual(tc.LinesNotCovered, 50);
        }

        [TestMethod]
        public void TestCoverage_DeserializeTestCoverage_Test()
        {
            var query = @"https://dltc.visualstudio.com/DefaultCollection/Deep Learning/_apis/test/codeCoverage?api-version=2.0-preview.1&buildId=1370&flags=1";
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();
            var response = VSOApiUtil.GetResponse(query, token);

            var listResponse = JsonConvert.DeserializeObject<TestCoverageListResponse>(response);
            Assert.IsTrue(listResponse != null);
            Assert.AreEqual(listResponse.count, 1);
            Assert.AreEqual(listResponse.count, listResponse.value.Count);
            Assert.AreEqual(listResponse.value[0].modules.Count, 6);
        }

        [TestMethod]
        public void TestCoverage_GetTestCoverageModel_Test()
        {
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();

            var testCoverage = TestCoverageUtil.GetTestCoverageForBuild("dltc", "Deep Learning", 1370, token);
            Assert.AreEqual(testCoverage.Count, 6);
        }

        [TestMethod]
        public void TestCoverage_MetricCollector_Test()
        {
            var writer = new TestCoverageDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var collector = new TestCoverageDBMetricCollector(writer, this.config);
            collector.CollectMetric();
        }
    }
}