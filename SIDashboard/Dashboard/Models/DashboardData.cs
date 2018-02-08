using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public enum MetricSummaryAlertLevel
    {
        NotApplicable,
        Fail,
        Warning,
        Pass
    }

    public class DashboardData
    {
        public string DateLabel { get; set; }

        public double CurrMonthNumber { get; set; }

        public double PrevMonthNumber { get; set; }

        public double OlderMonthNumber { get; set; }

        public MetricSummaryAlertLevel CurrentMonthAlertLevel { get; set; }

        public MetricSummaryAlertLevel PreviousMonthAlertLevel { get; set; }

        public MetricSummaryAlertLevel OlderMonthAlertLevel { get; set; }
    }
}