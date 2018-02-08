using Dashboard.Shared;
using Microsoft.BusinessAI.Dashboard.Common;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.DashboardDataReader
{
    public class CustomerCareIntelligenceDashboardDataReaderFactory : BaseDashboardDataReaderFactory
    {
        private Dictionary<UIMetricName, IDashboardDataReader> readerMap = new Dictionary<UIMetricName, IDashboardDataReader>
                {
                    { UIMetricName.OfficialBuild, new OfficialBuildDBDashboardDataReader(CustomerCareIntelligenceWebApiConstant.BizDashboardDBConnectionString) }
                };

        public override IDashboardDataReader GetReader(UIMetricName type)
        {
            return readerMap[type];
        }
    }
}