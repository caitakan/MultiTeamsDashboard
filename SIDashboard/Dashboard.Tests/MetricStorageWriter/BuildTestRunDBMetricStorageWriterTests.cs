using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dashboard.Tests.MetricStorageWriter
{
    [TestClass]
    public class ReleaseTestRunDBMetricStorageWriterUnitTest
    {
        private SolutionConfig config = null;

        [TestInitialize]
        public void InitializeWriterTestClass()
        {
            this.config = SolutionConfigManager.GetSolutionConfig(SolutionName.SalesIntelligence);
            this.config.OverrideDashboardDBConnection(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV].ToString());
        }

        [TestMethod]
        public void ReleaseTestRun_Insert_Test()
        {
            var writer = new ReleaseTestRunDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var detail = new Dictionary<string, object>();

            var runId = 100000;
            detail["VSO"] = "testVSO";
            detail["Project"] = "testProject";
            detail["BuildOrReleaseId"] = 100000;
            detail["RunId"] = runId;
            detail["PassedTestNum"] = 10;
            detail["TotalTestNum"] = 10;
            detail["CreationDate"] = DateTime.Parse("2017-01-01");

            var m1 = new Metric("TestRun", MetricType.TestRun, detail);

            writer.Write(m1);
            var tc = DBUtil.GetTestRun(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString(), runId);

            Assert.AreEqual(tc.VSO, "testVSO");
            Assert.AreEqual(tc.Project, "testProject");
            Assert.AreEqual(tc.BuildOrReleaseId, 100000);
            Assert.AreEqual(tc.RunId, runId);
            Assert.AreEqual(tc.PassedTestNum, 10);
            Assert.AreEqual(tc.TotalTestNum, 10);
            Assert.AreEqual(tc.CreationDate, DateTime.Parse("2017-01-01"));
        }

        [TestMethod]
        public void ReleaseTestRun_DeserializeTestRun_Test()
        {
            var query = @"https://bizai.visualstudio.com/BizAI/_apis/test/ResultSummaryByRelease?releaseEnvId=0&releaseId=81";
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();

            var response = VSOApiUtil.GetResponse(query, token);

            var listResponse = JsonConvert.DeserializeObject<ResultSummaryResponse>(response);
            Assert.IsTrue(listResponse != null);
            Assert.IsNotNull(listResponse.aggregatedResultsAnalysis);
            Assert.IsNotNull(listResponse.aggregatedResultsAnalysis.resultsByOutcome);
            Assert.IsNull(listResponse.aggregatedResultsAnalysis.resultsByOutcome.Failed);
            Assert.IsNotNull(listResponse.aggregatedResultsAnalysis.resultsByOutcome.Passed);
            Assert.AreEqual(1, listResponse.aggregatedResultsAnalysis.resultsByOutcome.Passed.count);
        }

        [TestMethod]
        public void ReleaseTestRun_GetTestRunModel_Test()
        {
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();

            var TestRun = TestRunUtil.GetReleaseTestRunDataModel("bizai", "BizAI", "0", 81, DateTime.Parse("2017-10-01"), token);
            Assert.AreEqual(1, TestRun.PassedTestNum);
        }

        [TestMethod]
        public void ReleaseTestRun_MetricCollector_Test()
        {
            var writer = new ReleaseTestRunDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            
            var collector = new ReleaseTestRunDBMetricCollector(writer, this.config);
            collector.CollectMetric();
        }
    }
}