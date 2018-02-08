using Microsoft.BusinessAI.Dashboard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.MetricSummaryRequestHandler
{
    public class MetricSummaryRequestHandlerFactory
    {
        private readonly static Dictionary<SolutionName, IMetricSummaryRequestHandler> map = new Dictionary<SolutionName, IMetricSummaryRequestHandler>()
        {
            {  SolutionName.SalesIntelligence, new SalesIntelligenceMetricSummaryRequestHandler() },
            {  SolutionName.CustomerCareIntelligence, new CustomerCareIntelligenceMetricSummaryRequestHandler() }
        };

        public static IMetricSummaryRequestHandler GetMetricSummaryRequestHandler(SolutionName solution)
        {
            return map[solution];
        }
    }
}