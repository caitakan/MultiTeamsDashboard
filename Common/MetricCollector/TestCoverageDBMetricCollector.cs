using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public class TestCoverageDBMetricCollector : BaseMetricCollector
    {
        public TestCoverageDBMetricCollector(IMetricStorageWriter writer, SolutionConfig config)
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
            var builIds = this.config[SolutionConfigName.OFFICIAL_BUILD_ID].ToString();

            var builds = OfficialBuildUtil.GetOfficialBuildDBModel(vso, project, builIds, begin, end, token);
            var metricList = new List<Metric>();

            foreach (var build in builds)
            {
                var coverage = TestCoverageUtil.GetTestCoverageForBuild(this.config[SolutionConfigName.VSO_NAME].ToString(), this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString(), build.BuildId, token);
                foreach (var item in coverage)
                {
                    var detail = new Dictionary<string, object>();
                    detail["VSO"] = item.VSO;
                    detail["Project"] = item.Project;
                    detail["BuildId"] = build.BuildId;
                    detail["ModuleName"] = item.ModuleName;
                    detail["LinesCovered"] = item.LinesCovered;
                    detail["LinesNotCovered"] = item.LinesNotCovered;
                    var metric = new Metric("TestCoverage", MetricType.TestCoverage, detail);
                    metricList.Add(metric);
                }
            }
            writer.Write(metricList);
        }
    }
}
