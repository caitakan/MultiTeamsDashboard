using Dashboard.DashboardDataReader;
using Dashboard.MetricSummaryRequestHandler;
using Dashboard.Models;
using Dashboard.Shared;
using Microsoft.BusinessAI.Dashboard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dashboard.Controllers
{
    public class MetricSummaryController : ApiController
    {
        // GET: api/MetricSummary
        public IEnumerable<MetricSummary> Get()
        {
            var parameters = Request.GetQueryNameValuePairs();
            var solutionName = parameters.FirstOrDefault(r => r.Key == "SolutionName").Value;
            var solution = (SolutionName)Enum.Parse(typeof(SolutionName), solutionName);
            var result = new List<MetricSummary>();

            var handler = MetricSummaryRequestHandlerFactory.GetMetricSummaryRequestHandler(solution);
            return (handler != null) ? handler.GetMetricSummary() : new MetricSummary[0];
        }
    }
}
