using Dashboard.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Dashboard.DashboardDataReader
{
    public class LatencyKustoDashboardDataReader : KustoDashboardDataReader
    {

        public LatencyKustoDashboardDataReader(string name, string kustoAccessKey, string kustoApiEndpoint) :
                base(name, kustoAccessKey, kustoApiEndpoint)
        {
        }
        
        protected override MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number)
        {
            const double PASS_NUMBER = 250;
            const double WARNING_NUMBER = 500;

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
    }
}