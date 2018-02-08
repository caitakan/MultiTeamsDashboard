using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public class BuildTestRunDBMetricCollector : BaseMetricCollector
    {
        public BuildTestRunDBMetricCollector(IMetricStorageWriter writer, SolutionConfig config)
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
            var buildIds = this.config[SolutionConfigName.OFFICIAL_BUILD_ID].ToString();

            var maxBuildId = OfficialBuildUtil.GetMaxBuildId(begin, end, vso, project, buildIds, token);
            var metricList = new List<Metric>();
            //Retrieve the previous 100 Test coverage info
            if (maxBuildId != -1)
            {
                var initBuildId = (maxBuildId - 100) < 1 ? 1 : maxBuildId - 100;

                for (var buildId = initBuildId; buildId <= maxBuildId; ++buildId)
                {
                    var runs = TestRunUtil.GetBuildTestRunDataModel(this.config[SolutionConfigName.VSO_NAME].ToString(), this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString(), buildId, token);
                    foreach (var item in runs)
                    {
                        var detail = new Dictionary<string, object>();
                        detail["VSO"] = item.VSO;
                        detail["Project"] = item.Project;
                        detail["BuildOrReleaseId"] = buildId;
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
}

