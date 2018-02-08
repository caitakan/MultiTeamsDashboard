using System;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    interface IMetricCollectorFactory
    {
        IMetricCollector GetMetricCollector(string type);
    }

    class MetricCollectorFactory : IMetricCollectorFactory
    {
        public IMetricCollector GetMetricCollector(string type)
        {
            throw new NotImplementedException();
        }
    }
}
