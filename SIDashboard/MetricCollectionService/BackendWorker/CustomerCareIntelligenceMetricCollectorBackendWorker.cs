using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.MetricCollectionService.BackendWorkder
{
    public class CustomerCareIntelligenceMetricCollectorBackendWorker : IMetricCollectorBackendWorker
    {
        OfficialBuildDBMetricCollector buildCollector;

        public CustomerCareIntelligenceMetricCollectorBackendWorker(SolutionConfig config)
        {
            APIPingUtil.LanuchPingThread(config[SolutionConfigName.DASHBOARD_APP_INSIGHT_KEY].ToString());

            var dbConnString = config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString();

            //Create build info collector
            var buildWriter = new OffcialBuildDBMetricStorageWriter(dbConnString);
            this.buildCollector = new OfficialBuildDBMetricCollector(buildWriter, config);
        }

        public void CollectMetrics()
        {
            //Collecting Metric
            this.buildCollector.CollectMetric();
        }
    }
}
