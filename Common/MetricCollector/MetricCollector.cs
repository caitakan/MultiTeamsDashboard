using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    interface IMetricCollector
    {
        void CollectMetric();
    }

    public abstract class BaseMetricCollector : IMetricCollector
    {
        protected IMetricStorageWriter writer;
        protected SolutionConfig config;

        public BaseMetricCollector(IMetricStorageWriter writer, SolutionConfig config)
        {
            this.writer = writer;
            this.config = config;
        }

        public abstract void CollectMetric();
    }
}
