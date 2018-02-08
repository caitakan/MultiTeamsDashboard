using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System.Collections.Generic;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    public interface IMetricStorageWriter
    {
        void Write(Metric metric);
        void Write(List<Metric> metricList);
    }
}
