using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class ResultSummaryResponse
    {
        public AggregatedResultsAnalysisResponse aggregatedResultsAnalysis { get; set; }
    }

    public class AggregatedResultsAnalysisResponse
    {
        public int totalTests { get; set; }
        public TimeSpan duration { get; set; }
        public ResultsByOutcomeResponse resultsByOutcome { get; set; }
    }

    public class ResultsByOutcomeResponse
    {
        public TestResultSummaryResponse Passed { get; set; }
        public TestResultSummaryResponse Failed { get; set; }
    }

    public class TestResultSummaryResponse
    {
        public string outcome { get; set; }
        public int count { get; set; }
        public TimeSpan duration { get; set; }
    }

}
