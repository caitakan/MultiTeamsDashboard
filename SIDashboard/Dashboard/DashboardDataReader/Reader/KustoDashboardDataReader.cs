using Dashboard.Models;
using Dashboard.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Dashboard.DashboardDataReader
{
    public class KustoDashboardDataReader : BaseDashboardDataReader
    {
        private string name = null;
        private string kustoAccessKey = null;
        private string kustoApiEndpoint = null;
        private Dictionary<string, string> templateMapping = new Dictionary<string, string>();
        public KustoDashboardDataReader(string name, string kustoAccessKey, string kustoApiEndpoint)
        {
            this.name = name;
            this.kustoAccessKey = kustoAccessKey;
            this.kustoApiEndpoint = kustoApiEndpoint;

            templateMapping[SalesIntelligenceWebApiConstant.PING_SERVICE_UPTIME] = SalesIntelligenceWebApiConstant.PING_KUSTO_REQUEST_BODY_TEMPLATE;
            templateMapping[SalesIntelligenceWebApiConstant.QUEUE_SCORING_SERVICE_UPTIME] = SalesIntelligenceWebApiConstant.QUEUE_SCORING_KUSTO_REQUEST_BODY_TEMPLATE;
            templateMapping[SalesIntelligenceWebApiConstant.LATENCY] = SalesIntelligenceWebApiConstant.LATENCY_KUSTO_REQUEST_BODY_TEMPLATE;
        }

        protected override MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number)
        {
            const double PASS_NUMBER = 0.999;
            const double WARNING_NUMBER = 0.998;

            if (number >= PASS_NUMBER)
            {
                return MetricSummaryAlertLevel.Pass;
            }
            else if (number > WARNING_NUMBER)
            {
                return MetricSummaryAlertLevel.Warning;
            }
            else
            {
                return MetricSummaryAlertLevel.Fail;
            }
        }

        private string GetRequestBodyTemplate()
        {
            return this.templateMapping[this.name];
        }

        public override DashboardData read()
        {
            var dashboard = new DashboardData();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("X-Api-Key", kustoAccessKey);

                var template = GetRequestBodyTemplate();
                var now = DateTime.Now;
                var prevMonth = now.AddMonths(-1);
                var nextMonth = now.AddMonths(1);
                var oldTimeEnd = (new DateTime(prevMonth.Year, prevMonth.Month, 1)).ToString("yyyy-MM-dd");
                var prevMonthEnd = (new DateTime(now.Year, now.Month, 1)).ToString("yyyy-MM-dd");
                var currMonthEnd = (new DateTime(nextMonth.Year, nextMonth.Month, 1)).ToString("yyyy-MM-dd");

                var jsonBody = string.Format(template, oldTimeEnd, prevMonthEnd, currMonthEnd);

                var stringContent = new StringContent(jsonBody,
                         Encoding.UTF8,
                         "application/json");

                using (HttpResponseMessage response = client.PostAsync(this.kustoApiEndpoint, stringContent).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    if (data != null && data.Tables != null && data.Tables[0] != null && data.Tables[0].Rows != null && data.Tables[0].Rows[0] != null)
                    {
                        var row = data.Tables[0].Rows[0];

                        var oldNumerator = (double)(row[0]);
                        var oldDenominator = (double)(row[1]);

                        var prevNumerator = (double)(row[2]);
                        var prevDenominator = (double)(row[3]);

                        var currNumerator = (double)(row[4]);
                        var currDenominator = (double)(row[5]);

                        dashboard.OlderMonthNumber = (oldDenominator == 0) ? 0 : oldNumerator / oldDenominator;
                        dashboard.PrevMonthNumber = (prevDenominator == 0) ? 0 : prevNumerator / prevDenominator;
                        dashboard.CurrMonthNumber = (currDenominator == 0) ? 0 : currNumerator / currDenominator;
                    }
                }//End of using
            }
            PopulateDashboardAlertLevel(dashboard);
            return dashboard;
        }
    }
}