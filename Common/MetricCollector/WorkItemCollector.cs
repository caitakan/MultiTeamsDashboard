using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using System;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public abstract class WorkItemCollector : BaseMetricCollector
    {
        public WorkItemCollector(IMetricStorageWriter writer, SolutionConfig config)
            : base(writer, config)
        {
        }
                
        public override void CollectMetric()
        {
            var vso = this.config[SolutionConfigName.VSO_NAME].ToString();
            var project = this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString();
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();
            var areaPath = this.config[SolutionConfigName.INCIDENT_VSO_AREA_PATH].ToString();

            var now = DateTime.Now;
            var end = now.AddDays(1).Date;
            var start = now.AddDays(-5).Date;
            this.CollectWorkItem(vso, project, areaPath, start, end, token);
        }

        public abstract void CollectWorkItem(string vso, string project, string areaPath, DateTime start, DateTime end, string token);
    }
}
