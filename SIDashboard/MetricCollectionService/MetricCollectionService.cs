using Microsoft.BusinessAI.Dashboard.Common.MetricCollector;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using Microsoft.BusinessAI.Dashboard.MetricCollectionService.BackendWorkder;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class MetricCollectionService : StatelessService
    {
        public MetricCollectionService(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            var solutionName = Environment.GetEnvironmentVariable("SolutionName");
            var solution = (SolutionName)Enum.Parse(typeof(SolutionName), solutionName);
     
            var solutionConfig = SolutionConfigManager.GetSolutionConfig(solution);

            AppInsightLogger.Initialize(solutionConfig[SolutionConfigName.DASHBOARD_APP_INSIGHT_KEY].ToString());
            long iterations = 0;

            var backendWorker = MetricCollectorWorkerFactory.GetMetricCollectorWorker(solution, solutionConfig);

            while (true)
            {
                AppInsightLogger.Instance.TraceIteration();
                cancellationToken.ThrowIfCancellationRequested();
                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                try
                {
                    backendWorker.CollectMetrics();
                }
                catch (Exception ex)
                {
                    AppInsightLogger.Instance.Error(ex);
                }

                await Task.Delay(TimeSpan.FromMinutes(3), cancellationToken);
            }
        }
    }
}
