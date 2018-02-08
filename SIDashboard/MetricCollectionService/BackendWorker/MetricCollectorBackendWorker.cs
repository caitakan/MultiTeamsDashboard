using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.MetricCollectionService.BackendWorkder
{
    public interface IMetricCollectorBackendWorker
    {
        void CollectMetrics();
    }
}
