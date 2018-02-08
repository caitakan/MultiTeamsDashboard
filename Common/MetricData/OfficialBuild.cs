using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class OfficialBuildListResponse
    {
        public int count { get; set; }
        public List<OfficialBuildResponse> value { get; set; }
    }

    public class OfficialBuildResponse
    {
        public int id { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public string queueTime { get; set; }
        public string sourceBranch { get; set; }

    }
}
