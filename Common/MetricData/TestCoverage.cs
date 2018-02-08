using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class TestCoverageListResponse
    {
        public int count { get; set; }
        public List<TestCoverageModuleListResponse> value { get; set; }
    }

    public class TestCoverageModuleListResponse
    {
        public List<TestCoverageResponse> modules { get; set; }
    }

    public class TestCoverageResponse
    {
        public string name { get; set; }
        public TestCoverageStatResponse statistics { get; set; }
    }

    public class TestCoverageStatResponse
    {
        public int linesCovered { get; set; }
        public int linesNotCovered { get; set; }
    }
}
