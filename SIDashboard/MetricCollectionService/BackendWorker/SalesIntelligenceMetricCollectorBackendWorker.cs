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
    public class SalesIntelligenceMetricCollectorWorker : IMetricCollectorBackendWorker
    {
        OfficialBuildDBMetricCollector buildCollector;
        PullRequestDBMetricCollector prCollector;
        TestCoverageDBMetricCollector testCoverageCollector;
        BuildTestRunDBMetricCollector buildTestRunCollector;
        ReleaseTestRunDBMetricCollector releaseTestRunCollector;
        VSOWorkItemCollector vsoWorkItemCollector;

        public SalesIntelligenceMetricCollectorWorker(SolutionConfig config)
        {
            APIPingUtil.LanuchPingThread(config[SolutionConfigName.DASHBOARD_APP_INSIGHT_KEY].ToString());

            var dbConnString = config[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR].ToString();

            //Create build info collector
            var buildWriter = new OffcialBuildDBMetricStorageWriter(dbConnString);
            this.buildCollector = new OfficialBuildDBMetricCollector(buildWriter, config);

            //Create PR info collector
            var prWriter = new PullRequestDBMetricStorageWriter(dbConnString);
            this.prCollector = new PullRequestDBMetricCollector(prWriter, config);

            //Create TestCoverage info collector
            var testCoverageWriter = new TestCoverageDBMetricStorageWriter(dbConnString);
            this.testCoverageCollector = new TestCoverageDBMetricCollector(testCoverageWriter, config);

            //Create TestRun collector, it collect result of test run triggered by build
            var buildTestrunWriter = new BuildTestRunDBMetricStorageWriter(dbConnString);
            this.buildTestRunCollector = new BuildTestRunDBMetricCollector(buildTestrunWriter, config);

            //Create TestRun collector, it collect result of test run triggered by build
            var releaseTestRunWriter = new ReleaseTestRunDBMetricStorageWriter(dbConnString);
            this.releaseTestRunCollector = new ReleaseTestRunDBMetricCollector(releaseTestRunWriter, config);

            //Create vso work item collector, it collects CriticalError and Customer Incident manaully tracked in VSO
            var vsoWorkItemWriter = new VSOWorkItemDBMetricStorageWriter(dbConnString);
            this.vsoWorkItemCollector = new VSOWorkItemCollector(vsoWorkItemWriter, config);

        }

        public void CollectMetrics()
        {
            //Collecting Metric
            this.buildCollector.CollectMetric();
            this.prCollector.CollectMetric();
            this.testCoverageCollector.CollectMetric();
            this.buildTestRunCollector.CollectMetric();
            this.releaseTestRunCollector.CollectMetric();
            this.vsoWorkItemCollector.CollectMetric();
        }
    }
}
