using Dashboard.DashboardDataReader;
using Dashboard.Models;
using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System.Collections.Generic;

namespace Dashboard.MetricSummaryRequestHandler
{
    public class SalesIntelligenceMetricSummaryRequestHandler : IMetricSummaryRequestHandler
    {
        public IEnumerable<MetricSummary> GetMetricSummary()
        {
            var readerFactory = BaseDashboardDataReaderFactory.GetReaderFactory(SolutionName.SalesIntelligence);

            var pr = readerFactory.GetReader(UIMetricName.PullRequest).read();
            var build = readerFactory.GetReader(UIMetricName.OfficialBuild).read();
            var bvt = readerFactory.GetReader(UIMetricName.BVT).read();
            var pingHealth = readerFactory.GetReader(UIMetricName.ServiceUpTime).read();
            var qssHealth = readerFactory.GetReader(UIMetricName.LeadScoringAPIUptime).read();
            var latency = readerFactory.GetReader(UIMetricName.Latency).read();
            var testCoverage = readerFactory.GetReader(UIMetricName.TestCoverage).read();
            var alertError = readerFactory.GetReader(UIMetricName.AlertError).read();
            var testCaseIdentifiedAndEnabled = readerFactory.GetReader(UIMetricName.TestCaseEnabled).read();
            var functionalTestPassRate = readerFactory.GetReader(UIMetricName.FunctionalTestPassRate).read();
            var customerIncidents = readerFactory.GetReader(UIMetricName.CustomerIncident).read();
            var sevAIncidents = readerFactory.GetReader(UIMetricName.SevAIncident).read();
            var incidentResolveTime = readerFactory.GetReader(UIMetricName.IncidentResolveTime).read();
            var incidentCloseTime = readerFactory.GetReader(UIMetricName.IncidentCloseTime).read();
            var outOfSLA = readerFactory.GetReader(UIMetricName.OutOfSLA).read();

            var e2eResponseData = DeriveE2EResponseDashboardData(latency);
            var throughput = DeriveThroughputDashboardData(latency);

            return new MetricSummary[] {
                new MetricSummary("Code reviewed and signed off",
                    pr,
                    2,
                    null,
                    true),

                new MetricSummary("Official Build Failures",
                    build,
                    0,
                    null),

                new MetricSummary("Build Verfication Pass Rate",
                    bvt,
                    2,
                    null,
                    true),

                new MetricSummary("Service Up time - Ping Health",
                    pingHealth,
                    6,
                    null,
                    true),

                new MetricSummary("Service Up time - Queue Scoring Health",
                    qssHealth,
                    6,
                    null),

                new MetricSummary("Test Coverage C#",
                    testCoverage,
                    2,
                    null,
                    true),

                new MetricSummary("Latency",
                    latency,
                    2,
                    null),

                 new MetricSummary("End-To-End Response Time",
                    e2eResponseData,
                    2,
                    null),

                 new MetricSummary("Throughput",
                    throughput,
                    0,
                    null),

                new MetricSummary("Critical Errors",
                    alertError,
                    2,
                    null),

                new MetricSummary("Test Cases Identified & Enabled",
                    testCaseIdentifiedAndEnabled,
                    0,
                    null
                    ),

                new MetricSummary("Feature Verification Test Pass Rate",
                    functionalTestPassRate,
                    2,
                    null,
                    true
                    ),

                new MetricSummary("Customer Incidents",
                    customerIncidents,
                    1,
                    null
                    ),

                new MetricSummary("Number of Sev A Incidents",
                    sevAIncidents,
                    1,
                    null
                    ),

                 new MetricSummary("Mean time to Fix Sev A",
                    incidentResolveTime,
                    1,
                    null
                    ),

                  new MetricSummary("Mean time to Deploy Sev A",
                    incidentCloseTime,
                    1,
                    null
                    ),

                  new MetricSummary("Number of Customers out of SLA",
                    outOfSLA,
                    1,
                    null
                    )
            };
        }

        private DashboardData DeriveE2EResponseDashboardData(DashboardData latencyData)
        {
            var e2eMetric = new DashboardData();
            e2eMetric.CurrMonthNumber = latencyData.CurrMonthNumber / 1000;
            e2eMetric.PrevMonthNumber = latencyData.PrevMonthNumber / 1000;
            e2eMetric.OlderMonthNumber = latencyData.OlderMonthNumber / 1000;

            e2eMetric.CurrentMonthAlertLevel = GetE2EResponseMetricSummaryAlertLevel(e2eMetric.CurrMonthNumber);
            e2eMetric.PreviousMonthAlertLevel = GetE2EResponseMetricSummaryAlertLevel(e2eMetric.PrevMonthNumber);
            e2eMetric.OlderMonthAlertLevel = GetE2EResponseMetricSummaryAlertLevel(e2eMetric.OlderMonthNumber);

            return e2eMetric;
        }
        private MetricSummaryAlertLevel GetE2EResponseMetricSummaryAlertLevel(double number)
        {
            const double PASS_NUMBER = 2;
            const double WARNING_NUMBER = 5;

            if (number <= PASS_NUMBER)
            {
                return MetricSummaryAlertLevel.Pass;
            }
            else if (number <= WARNING_NUMBER)
            {
                return MetricSummaryAlertLevel.Warning;
            }
            else
            {
                return MetricSummaryAlertLevel.Fail;
            }
        }

        private DashboardData DeriveThroughputDashboardData(DashboardData latencyData)
        {
            var throughputMetric = new DashboardData();
            const int NUM_SLS_APP_INSTANCES = 3;
            throughputMetric.CurrMonthNumber = (1000 / latencyData.CurrMonthNumber) * NUM_SLS_APP_INSTANCES;
            throughputMetric.PrevMonthNumber = (1000 / latencyData.PrevMonthNumber) * NUM_SLS_APP_INSTANCES;
            throughputMetric.OlderMonthNumber = (1000 / latencyData.OlderMonthNumber) * NUM_SLS_APP_INSTANCES;

            throughputMetric.CurrentMonthAlertLevel = GetThroughputMetricSummaryAlertLevel(throughputMetric.CurrMonthNumber);
            throughputMetric.PreviousMonthAlertLevel = GetThroughputMetricSummaryAlertLevel(throughputMetric.PrevMonthNumber);
            throughputMetric.OlderMonthAlertLevel = GetThroughputMetricSummaryAlertLevel(throughputMetric.OlderMonthNumber);

            return throughputMetric;
        }

        private MetricSummaryAlertLevel GetThroughputMetricSummaryAlertLevel(double number)
        {
            const double PASS_NUMBER = 100;
            const double WARNING_NUMBER = 80;

            if (number >= PASS_NUMBER)
            {
                return MetricSummaryAlertLevel.Pass;
            }
            else if (number >= WARNING_NUMBER)
            {
                return MetricSummaryAlertLevel.Warning;
            }
            else
            {
                return MetricSummaryAlertLevel.Fail;
            }
        }
    }
}