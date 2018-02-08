using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public enum UIMetricName
    {
        PullRequest,
        OfficialBuild,
        TestCoverage,
        AlertError,
        BVT,
        ServiceUpTime,
        LeadScoringAPIUptime,
        Latency,
        TestCaseEnabled,
        FunctionalTestPassRate,
        CustomerIncident,
        SevAIncident,
        IncidentResolveTime,
        IncidentCloseTime,
        OutOfSLA
    }

    public enum MetricType
    {
        PullRequest,
        OfficialBuild,
        TestCoverage,
        TestRun,
        VsoWorkItem
    }
}
