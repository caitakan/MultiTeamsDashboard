using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    public class OffcialBuildDBMetricStorageWriter : DBMetricStorageWriter
    {
        public OffcialBuildDBMetricStorageWriter(string connectionString)
            : base(connectionString)
        {
        }

        public override void Write(List<Metric> metricList)
        {
            var table = new DataTable();
            table.Columns.Add("VSO", typeof(string));
            table.Columns.Add("Project", typeof(string));
            table.Columns.Add("BuildId", typeof(Int32));
            table.Columns.Add("Result", typeof(bool));
            table.Columns.Add("SourceBranch", typeof(string));
            table.Columns.Add("CreationDate", typeof(DateTime));

            // Add New Rowto table
            foreach (var metric in metricList)
            {
                var vso = metric.GetDetail("VSO");
                var project = metric.GetDetail("Project");
                var buildId = metric.GetDetail("BuildId");
                var branch = metric.GetDetail("SourceBranch");
                var succeed = metric.GetDetail("Result");
                var date = DateTime.Parse(metric.GetDetail("CreationDate").ToString());

                //var date = DateTime.Parse(DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"));

                table.Rows.Add(vso, project, buildId, succeed, branch, date);
            }
            DBUtil.ExecuteMetricMergeQuery(ConnectionString, "prc_MergeOfficialBuild", "@newBuildInfo", table);
        }
    }
}
