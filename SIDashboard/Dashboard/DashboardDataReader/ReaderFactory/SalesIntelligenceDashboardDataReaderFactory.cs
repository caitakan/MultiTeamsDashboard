using Dashboard.Shared;
using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.DashboardDataReader
{
    public class SalesIntelligenceDashboardDataReaderFactory : BaseDashboardDataReaderFactory
    {
        private Dictionary<UIMetricName, IDashboardDataReader> readerMap = new Dictionary<UIMetricName, IDashboardDataReader>
                {
                    { UIMetricName.PullRequest, new PullRequestDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.OfficialBuild, new OfficialBuildDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.BVT, new BVTDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.TestCoverage, new TestCoverageDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.AlertError, new AlertErrorDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.TestCaseEnabled, new TestRunDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.FunctionalTestPassRate, new FunctionTestPassRateDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.CustomerIncident, new CustomerIncidentDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.SevAIncident, new SevADBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.IncidentResolveTime, new CustomerIncidentResolveTimeDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.IncidentCloseTime, new CustomerIncidentCloseTimeDBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },
                    { UIMetricName.OutOfSLA, new OutOfSLADBDashboardDataReader(SalesIntelligenceWebApiConstant.BizDashboardDBConnectionString) },

                    { UIMetricName.ServiceUpTime, new ServiceUpTimeKustoDashboardDataReader(
                        SalesIntelligenceWebApiConstant.PING_SERVICE_UPTIME,
                        SalesIntelligenceWebApiConstant.PING_SERVICE_UPTIME_KUSTO_KEY,
                        SalesIntelligenceWebApiConstant.PING_SERVICE_UPTIME_KUSTO_ENDPOINT) },
                    { UIMetricName.LeadScoringAPIUptime, new ServiceUpTimeKustoDashboardDataReader(
                        SalesIntelligenceWebApiConstant.QUEUE_SCORING_SERVICE_UPTIME,
                        SalesIntelligenceWebApiConstant.INSIDE_SALES_KUSTO_KEY,
                        SalesIntelligenceWebApiConstant.INSIDE_SALES_KUSTO_ENDPOINT)},
                    { UIMetricName.Latency, new LatencyKustoDashboardDataReader(
                        SalesIntelligenceWebApiConstant.LATENCY,
                        SalesIntelligenceWebApiConstant.INSIDE_SALES_KUSTO_KEY,
                        SalesIntelligenceWebApiConstant.INSIDE_SALES_KUSTO_ENDPOINT) }
                };

        public override IDashboardDataReader GetReader(UIMetricName type)
        {
            return readerMap[type];
        }
    }
}