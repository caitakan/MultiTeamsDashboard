using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class ReleaseListResponse
    {
        public List<ReleaseResponse> value { get; set; }
    }

    public class ReleaseResponse
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }

        public List<ReleaseEnvironmentResposne> environments { get; set; }
    }

    public class ReleaseEnvironmentResposne
    {
        public int id { get; set; }
        public string status { get; set; }
    }
}
