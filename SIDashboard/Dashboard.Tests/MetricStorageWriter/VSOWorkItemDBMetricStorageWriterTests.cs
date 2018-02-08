using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System.Collections.Generic;
using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common;

namespace Dashboard.Tests.MetricStorageWriter
{
    [TestClass]
    public class VSOWorkItemDBMetricStorageWriterTests
    {
        private SolutionConfig config = null;

        [TestInitialize]
        public void InitializeWriterTestClass()
        {
            this.config = SolutionConfigManager.GetSolutionConfig(SolutionName.SalesIntelligence);
            this.config.OverrideDashboardDBConnection(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV].ToString());
        }

        [TestMethod]
        public void VSOWorkItem_INSERT_Test()
        {
            var writer = new VSOWorkItemDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var detail = new Dictionary<string, object>();
            const int id = 99999;

            detail["Id"] = id;
            detail["VSO"] = "myvso";
            detail["Project"] = "myProject";
            detail["System.Title"] = "myTitle";
            detail["System.Tags"] = "myTag1; myTag2";
            var m1 = new Metric("Alert", MetricType.PullRequest, detail);
            writer.Write(m1);

            var title = DBUtil.GetAlert(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString(), id).fields["Title"];
            Assert.AreEqual("myTitle", title);
        }

        [TestMethod]
        public void VSOWorkItem_GetVSOWorkItemBuildModel_Test()
        {
            var start = DateTime.Parse("2017-09-01");
            var end = DateTime.Parse("2017-10-01");

            var vso = this.config[SolutionConfigName.VSO_NAME].ToString();
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();
            var areapath = this.config[SolutionConfigName.INCIDENT_VSO_AREA_PATH].ToString();

            var buildModelList = WorkItemUtil.GetVSOWorkItemDBModel(vso, start, end, token, areapath);

            Assert.IsTrue(buildModelList.Count == 0);
        }

        [TestMethod]
        public void VSOWorkItem_MetricCollector_Test()
        {
            var writer = new VSOWorkItemDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var collector = new VSOWorkItemCollector(writer, this.config);
            collector.CollectMetric();
        }
    }
}

