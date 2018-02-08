using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using System.Collections.Generic;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Newtonsoft.Json;
using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common;

namespace Microsoft_Dashboard.Tests.MetricStorageWriter
{
    [TestClass]
    public class OffcialBuildDBMetricStorageWriterUnitTest
    {
        private SolutionConfig config = null;

        [TestInitialize]
        public void InitializeWriterTestClass()
        {
            this.config = SolutionConfigManager.GetSolutionConfig(SolutionName.SalesIntelligence);
            this.config.OverrideDashboardDBConnection(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV].ToString());
        }

        [TestMethod]
        public void OfficialBuild_Insert_Test()
        {
            var writer = new OffcialBuildDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var detail = new Dictionary<string, object>();
            const int id = 100000;
            const int success = 0;

            detail["VSO"] = "dltc";
            detail["Project"] = "myProj";
            detail["BuildId"] = id;
            detail["Result"] = success;
            detail["SourceBranch"] = "myBranch";
            detail["CreationDate"] = "2017-08-23T21:13:19.2470822Z";

            var m1 = new Metric("OfficialBuild", MetricType.OfficialBuild, detail);

            writer.Write(m1);
            var build = DBUtil.GetOfficialBuildModel(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString(), "dltc", "myProj", id);

            Assert.AreEqual(build.Result, false);
        }

        [TestMethod]
        public void OfficialBuild_DeserializeOfficialBuild__Test()
        {
            var query = @"https://dltc.visualstudio.com/DefaultCollection/Deep Learning/_apis/build/builds?api-version=2.0&minFinishTime=2015-08-15T00:00:00Z&maxFinishTime=2018-08-20T00:00:00Z&definitions=18";
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();

            var response = VSOApiUtil.GetResponse(query, token);

            var buildListResponse = JsonConvert.DeserializeObject<OfficialBuildListResponse>(response);
            Assert.IsTrue(buildListResponse.count > 0);
            Assert.AreEqual(buildListResponse.count, buildListResponse.value.Count);
        }

        [TestMethod]
        public void OfficialBuild_GetOfficialBuildModel_Test()
        {
            var query = @"https://dltc.visualstudio.com/DefaultCollection/Deep Learning/_apis/build/builds?api-version=2.0&minFinishTime=2015-08-15T00:00:00Z&maxFinishTime=2018-08-20T00:00:00Z&definitions=18";
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();

            var buildModelList = OfficialBuildUtil.GetOfficialBuildDBModel("dltc", "Deep Learning", query, token);
            Assert.IsTrue(buildModelList.Count > 0);
        }

        [TestMethod]
        public void OfficialBuild_MetricCollector_Test()
        {
            var writer = new OffcialBuildDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var collector = new OfficialBuildDBMetricCollector(writer, this.config);
            collector.CollectMetric();
        }        
    }
}