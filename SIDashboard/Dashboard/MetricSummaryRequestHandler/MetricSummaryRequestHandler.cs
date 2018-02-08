using Dashboard.Models;
using Microsoft.BusinessAI.Dashboard.Common;
using System.Collections.Generic;

namespace Dashboard.MetricSummaryRequestHandler
{
    public interface IMetricSummaryRequestHandler
    {
        IEnumerable<MetricSummary> GetMetricSummary();
    }
}