using Dashboard.DashboardDataReader;
using Dashboard.Models;
using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System.Collections.Generic;

namespace Dashboard.MetricSummaryRequestHandler
{
    public class CustomerCareIntelligenceMetricSummaryRequestHandler : IMetricSummaryRequestHandler
    {
        public IEnumerable<MetricSummary> GetMetricSummary()
        {
            var readerFactory = BaseDashboardDataReaderFactory.GetReaderFactory(SolutionName.CustomerCareIntelligence);

            var build = readerFactory.GetReader(UIMetricName.OfficialBuild).read();

            return new MetricSummary[] {
                new MetricSummary("Official Build Failures", build, 0, null)
            };
        }
    }
}