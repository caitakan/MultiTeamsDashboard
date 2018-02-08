using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class TestRunListResponse
    {
        public List<TestRunResponse> value { get; set; }
    }

    public class TestRunResponse
    {
        public int id { get; set; }
        public int totalTests { get; set; }
        public int passedTests { get; set; }
        public DateTime startedDate { get; set; }
    }
}
