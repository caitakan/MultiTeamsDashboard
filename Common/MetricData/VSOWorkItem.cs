using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class VSOWorkItemReferenceResponse
    {
        public int id { get; set; }
    }

    public class VSOWorkItemListResponse
    {
        public List<VSOWorkItemReferenceResponse> workItems { get; set; }
    }

    public class VSOResponse
    {
        public int id { get; set; }
        public Dictionary<string, object> fields { get; set; }
    }
}
