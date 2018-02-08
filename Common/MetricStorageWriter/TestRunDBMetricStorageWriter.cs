using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    public abstract class TestRunDBMetricStorageWriter : DBMetricStorageWriter
    {
        public TestRunDBMetricStorageWriter(string connectionString)
            : base(connectionString)
        {
        }

        protected abstract string TriggeredByValue { get; }

        public override void Write(List<Metric> metricList)
        {
            if (metricList == null || metricList.Count <= 0)
                return;

            var table = new DataTable();
            table.Columns.Add("VSO", typeof(string));
            table.Columns.Add("Project", typeof(string));
            table.Columns.Add("BuildOrReleaseId", typeof(Int32));
            table.Columns.Add("RunId", typeof(Int32));
            table.Columns.Add("PassedTestNum", typeof(Int32));
            table.Columns.Add("TotalTestNum", typeof(Int32));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("TriggeredBy", typeof(string));

            foreach (var metric in metricList)
            {
                var vso = metric.GetDetail("VSO");
                var project = metric.GetDetail("Project");
                var buildId = metric.GetDetail("BuildOrReleaseId");
                var moduleName = metric.GetDetail("RunId");
                var linesCovered = metric.GetDetail("PassedTestNum");
                var linesNotCovered = metric.GetDetail("TotalTestNum");
                var creationDate = metric.GetDetail("CreationDate");
                var triggeredBy = TriggeredByValue;
                table.Rows.Add(vso, project, buildId, moduleName, linesCovered, linesNotCovered, creationDate, triggeredBy);
            }
            DBUtil.ExecuteMetricMergeQuery(ConnectionString, "prc_MergeTestRun", "@newInfo", table);
        }
    }

    //======================================================================================
    public class BuildTestRunDBMetricStorageWriter : TestRunDBMetricStorageWriter
    {
        public BuildTestRunDBMetricStorageWriter(string connectionString)
            : base(connectionString)
        {
        }

        protected override string TriggeredByValue { get { return "Build"; } }
    }

    //======================================================================================
    public class ReleaseTestRunDBMetricStorageWriter : TestRunDBMetricStorageWriter
    {
        public ReleaseTestRunDBMetricStorageWriter(string connectionString)
            : base(connectionString)
        {
        }

        protected override string TriggeredByValue { get { return "Release"; } }
    }
}
