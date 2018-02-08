using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class MetricSummary
    {
        public MetricSummary(string name, DashboardData data, int decimalCount, string link, bool showAsPercentage = false)
        {
            this.MetricName = name;
            this.CurrentMonthValue = GetMetricString(data.CurrMonthNumber, decimalCount, showAsPercentage);
            this.PreviousMonthValue = GetMetricString(data.PrevMonthNumber, decimalCount, showAsPercentage);
            this.OlderMonthValue = GetMetricString(data.OlderMonthNumber, decimalCount, showAsPercentage);

            this.CurrentMonthAlertLevel = data.CurrentMonthAlertLevel;
            this.PreviousMonthAlertLevel = data.PreviousMonthAlertLevel;
            this.OlderMonthAlertLevel = data.OlderMonthAlertLevel;

            this.Deeplink = link;
        }

        private static string GetMetricString(double number, int decimalCount, bool showAsPercentage)
        {
            var n = showAsPercentage ? number * 100 : number;
            var suffix = showAsPercentage ? "%" : "";
            decimalCount = (showAsPercentage && (number == 1) || (number == 0)) ? 0 : decimalCount;
            return n.ToString("n" + decimalCount) + suffix;
        }

        public string MetricName;
        public string CurrentMonthValue;
        public string PreviousMonthValue;
        public string OlderMonthValue;
        public MetricSummaryAlertLevel CurrentMonthAlertLevel;
        public MetricSummaryAlertLevel PreviousMonthAlertLevel;
        public MetricSummaryAlertLevel OlderMonthAlertLevel;

        public string Deeplink;
    }
}