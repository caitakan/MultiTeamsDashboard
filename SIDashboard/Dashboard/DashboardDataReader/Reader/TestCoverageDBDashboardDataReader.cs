using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Models;
using System.Data.SqlClient;
using System.Data;
using Dashboard.Shared;

namespace Dashboard.DashboardDataReader
{
    public class TestCoverageDBDashboardDataReader : DBDashboardDataReader
    {
        public TestCoverageDBDashboardDataReader(string connectionString) : base(connectionString)
        {
        }

        protected override MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number)
        {
            const double PASS_NUMBER = 0.85;
            const double WARNING_NUMBER = 0.75;

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

        public override DashboardData read()
        {
            return ReadSingleColumnTable("prc_GetTestCoverageDashboard", "AvgTestCoverage");
        }
    }
}