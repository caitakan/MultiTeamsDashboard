using Dashboard.Shared;
using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.DashboardDataReader
{
    public abstract class BaseDashboardDataReaderFactory
    {
        private static Dictionary<SolutionName, BaseDashboardDataReaderFactory> readerFactory = new Dictionary<SolutionName, BaseDashboardDataReaderFactory>(){
                {SolutionName.SalesIntelligence, new SalesIntelligenceDashboardDataReaderFactory() },
                {SolutionName.CustomerCareIntelligence, new CustomerCareIntelligenceDashboardDataReaderFactory() }
            };

        public static BaseDashboardDataReaderFactory GetReaderFactory(SolutionName solution)
        {
            return readerFactory[solution];
        }

        public abstract IDashboardDataReader GetReader(UIMetricName type);
    }
}