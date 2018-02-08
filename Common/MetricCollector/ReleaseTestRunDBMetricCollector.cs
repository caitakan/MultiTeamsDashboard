using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public class ReleaseTestRunDBMetricCollector : BaseMetricCollector
    {
        public ReleaseTestRunDBMetricCollector(IMetricStorageWriter writer, SolutionConfig config)
            : base(writer, config)
        {
        }

        public override void CollectMetric()
        {
            var end = DateTime.Now.AddDays(1);
            var begin = end.AddDays(-5);
            var vso = this.config[SolutionConfigName.VSO_NAME].ToString();
            var project = this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString();
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();
            var releaseId = this.config[SolutionConfigName.FUNCTIONAL_TEST_RELEASE_DEFINITION_ID].ToString();
            var releases = ReleaseUtil.GetReleaseByReleaseDefinitionId(vso, project, begin, end, releaseId, token);

            var metricList = new List<Metric>();

            //Get release test result
            foreach (var rel in releases)
            {
                var item = TestRunUtil.GetReleaseTestRunDataModel(vso, project, releaseId, rel.id, rel.CreationDate, token);

                if (item != null)
                {
                    var detail = new Dictionary<string, object>();
                    detail["VSO"] = item.VSO;
                    detail["Project"] = item.Project;
                    detail["BuildOrReleaseId"] = rel.id;
                    detail["RunId"] = item.RunId;
                    detail["PassedTestNum"] = item.PassedTestNum;
                    detail["TotalTestNum"] = item.TotalTestNum;
                    detail["CreationDate"] = item.CreationDate;
                    var metric = new Metric("TestRun", MetricType.TestRun, detail);
                    metricList.Add(metric);
                }
            }
            writer.Write(metricList);
        }
    }
}

