using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dashboard.Tests.MetricStorageWriter
{
    [TestClass]
    public class OfficialBuildDBMetricStorageWriterUnitTest
    {
        private SolutionConfig config = null;

        [TestInitialize]
        public void InitializeWriterTestClass()
        {
            this.config = SolutionConfigManager.GetSolutionConfig(SolutionName.SalesIntelligence);
            this.config.OverrideDashboardDBConnection(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV].ToString());
        }

        [TestMethod]
        public void PR_Insert_Test()
        {
            var writer = new PullRequestDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var detail = new Dictionary<string, object>();
            const int prId = 100000;
            const int activeComment = 0;

            detail["VSO"] = "dltc";
            detail["Project"] = "myProj";
            detail["Repository"] = "MyRepo";
            detail["PRId"] = prId;
            detail["CountOfCommentNotFixed"] = activeComment;
            detail["CreationDate"] = "2017-08-23T21:13:19.2470822Z";

            var m1 = new Metric("PR", MetricType.PullRequest, detail);

            writer.Write(m1);
            var pr = DBUtil.GetPullRequest(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString(), "dltc", "myProj", prId);

            Assert.AreEqual(pr.CountOfCommentNotFixed, activeComment);
        }

        [TestMethod]
        public void PR_DeserializePR_Test()
        {
            var query = @"https://dltc.visualstudio.com/DefaultCollection/Deep Learning/_apis/git/repositories/Victoria/pullRequests?status=Completed";
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();
            var response = VSOApiUtil.GetResponse(query, token);

            var pullReqeustListResponse = JsonConvert.DeserializeObject<PullRequestListResponse>(response);
            Assert.IsTrue(pullReqeustListResponse.count > 0);
            Assert.AreEqual(pullReqeustListResponse.count, pullReqeustListResponse.value.Count);
        }

        [TestMethod]
        public void PR_GetPRModel_Test()
        {
            var query = @"https://bizai.visualstudio.com/DefaultCollection/BizAI/_apis/git/repositories/SalesIntelligence/pullRequests?status=Completed";
            var vso = this.config[SolutionConfigName.VSO_NAME].ToString();
            var project = this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString();
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();


            var prModelList = PullRequestUtil.GetPullRequestDBModel(
                vso,
                project,
                query,
                "SalesIntelligence",
                token);

            Assert.IsTrue(prModelList.Count > 0);
        }

        [TestMethod]
        public void PR_MetricCollector_Test()
        {
            var writer = new PullRequestDBMetricStorageWriter(this.config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString());
            var collector = new PullRequestDBMetricCollector(writer, this.config);
            collector.CollectMetric();
        }
    }
}