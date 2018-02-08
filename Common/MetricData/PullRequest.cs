using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class PullRequestCommentResponse
    {
        public int id { get; set; }

        public string status { get; set; }
        public bool isDeleted { get; set; }
    }

    public class PullRequestCommentListResponse
    {
        public int count { get; set; }
        public List<PullRequestCommentResponse> value { get; set; }
    }

    public class PullRequestResponse
    {
        public int pullRequestId { get; set; }
        public string title { get; set; }

        public string creationDate { get; set; }

    }

    public class PullRequestListResponse
    {
        public int count { get; set; }
        public List<PullRequestResponse> value { get; set; }
    }
}
