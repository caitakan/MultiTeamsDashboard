using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    public class PullRequestDBMetricStorageWriter : DBMetricStorageWriter
    {       
        public PullRequestDBMetricStorageWriter(string connectionString)
            : base(connectionString)
        {
        }

        public override void Write(List<Metric> metricList)
        {
            if ((metricList == null) || (metricList.Count <= 0))
                return;

            var table = new DataTable();
            table.Columns.Add("VSO", typeof(string));
            table.Columns.Add("Project", typeof(string));
            table.Columns.Add("Repository", typeof(string));
            table.Columns.Add("PRId", typeof(Int32));
            table.Columns.Add("CountOfCommentNotFixed", typeof(Int32));
            table.Columns.Add("CreationDate", typeof(DateTime));

            foreach (var metric in metricList)
            {
                var vso = metric.GetDetail("VSO");
                var project = metric.GetDetail("Project");
                var repo = metric.GetDetail("Repository");
                var prId = metric.GetDetail("PRId");
                var countOfCommentNotFixed = metric.GetDetail("CountOfCommentNotFixed");
                var date = DateTime.Parse(metric.GetDetail("CreationDate").ToString());

                table.Rows.Add(vso, project, repo, prId, countOfCommentNotFixed, date);
            }

            DBUtil.ExecuteMetricMergeQuery(ConnectionString, "prc_MergePullRequest", "@newPullRequestInfo", table);
        }
    }
}
