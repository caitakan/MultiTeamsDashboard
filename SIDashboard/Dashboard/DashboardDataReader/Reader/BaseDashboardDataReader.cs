using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Models;

namespace Dashboard.DashboardDataReader
{
    public abstract class BaseDashboardDataReader : IDashboardDataReader
    {
        protected void PopulateDashboardAlertLevel(DashboardData data)
        {
            data.CurrentMonthAlertLevel = GetMetricSummaryAlertLevel(data.CurrMonthNumber);
            data.PreviousMonthAlertLevel = GetMetricSummaryAlertLevel(data.PrevMonthNumber);
            data.OlderMonthAlertLevel = GetMetricSummaryAlertLevel(data.OlderMonthNumber);
        }


        protected abstract MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number);
        

        public abstract DashboardData read();
    }
}