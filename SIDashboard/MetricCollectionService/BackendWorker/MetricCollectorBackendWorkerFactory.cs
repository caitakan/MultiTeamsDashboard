using Microsoft.BusinessAI.Dashboard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.MetricCollectionService.BackendWorkder
{
    public class MetricCollectorWorkerFactory
    {
        public static IMetricCollectorBackendWorker GetMetricCollectorWorker(SolutionName solution, SolutionConfig config)
        {
            switch (solution)
            {
                case SolutionName.SalesIntelligence:
                    return new SalesIntelligenceMetricCollectorWorker(config);
                case SolutionName.CustomerCareIntelligence:
                    return new CustomerCareIntelligenceMetricCollectorBackendWorker(config);
                default:
                    return null;
            }
        }
    }
}
