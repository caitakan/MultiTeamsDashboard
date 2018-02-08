using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public class PullRequestDBMetricCollector : BaseMetricCollector
    {
        public PullRequestDBMetricCollector(IMetricStorageWriter writer, SolutionConfig config)
            : base(writer, config)
        {

        }

        public override void CollectMetric()
        {
            const int PR_PER_REQUEST = 50;
            const int NUM_REQUEST = 4;
            this.CollectPullRequestData(PR_PER_REQUEST, NUM_REQUEST);
        }

        public void CollectPullRequestData(int prPerRequest, int numOfRequest)
        {
            var metricList = new List<Metric>();
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();

            foreach (var repo in (this.config[SolutionConfigName.PULL_REQUEST_REPOSITORIES] as string[]))
            {
                var skip = 0;

                for (var i = 0; i < numOfRequest; ++i)
                {
                    var query = PullRequestUtil.GetQueryStringWithPaging(this.config[SolutionConfigName.VSO_NAME].ToString(),
                        this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString(),
                        repo,
                        prPerRequest,
                        skip,
                       "Completed"
                    );

                    skip += prPerRequest;

                    var pullRequestFromVSO = PullRequestUtil.GetPullRequestDBModel(
                        this.config[SolutionConfigName.VSO_NAME].ToString(),
                        this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString(),
                        query,
                        repo,
                        token);

                    foreach (var pr in pullRequestFromVSO)
                    {
                        var detail = new Dictionary<string, object>();
                        detail["VSO"] = pr.VSO;
                        detail["Project"] = pr.Project;
                        detail["Repository"] = repo;
                        detail["PRId"] = pr.PRId;
                        detail["CountOfCommentNotFixed"] = pr.CountOfCommentNotFixed;
                        detail["CreationDate"] = pr.CreationDate;

                        var metric = new Metric("PullRequest", MetricType.PullRequest, detail);
                        metricList.Add(metric);
                    }
                }
            }

            writer.Write(metricList);
        }
    }
}
