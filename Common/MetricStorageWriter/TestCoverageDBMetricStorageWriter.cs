using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    public class TestCoverageDBMetricStorageWriter : DBMetricStorageWriter
    {
        public TestCoverageDBMetricStorageWriter(string connectionString)
            : base(connectionString)
        {
        }

        public override void Write(List<Metric> metricList)
        {
            if (metricList == null || metricList.Count <= 0)
                return;

            var table = new DataTable();
            table.Columns.Add("VSO", typeof(string));
            table.Columns.Add("Project", typeof(string));
            table.Columns.Add("BuildId", typeof(Int32));
            table.Columns.Add("ModuleName", typeof(string));
            table.Columns.Add("LinesCovered", typeof(Int32));
            table.Columns.Add("LinesNotCovered", typeof(Int32));

            foreach (var metric in metricList)
            {
                var vso = metric.GetDetail("VSO");
                var project = metric.GetDetail("Project");
                var buildId = metric.GetDetail("BuildId");
                var moduleName = metric.GetDetail("ModuleName");
                var linesCovered = metric.GetDetail("LinesCovered");
                var linesNotCovered = metric.GetDetail("LinesNotCovered");

                table.Rows.Add(vso, project, buildId, moduleName, linesCovered, linesNotCovered);
            }

            DBUtil.ExecuteMetricMergeQuery(ConnectionString, "prc_MergeTestCoverage", "@newTestCoverageInfo", table);
        }
    }
}
