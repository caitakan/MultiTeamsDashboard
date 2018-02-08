using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using System.Data;
using Microsoft.BusinessAI.Dashboard.Common.Shared;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    public class VSOWorkItemDBMetricStorageWriter : DBMetricStorageWriter
    {
        public VSOWorkItemDBMetricStorageWriter(string connectionString)
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
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("State", typeof(string));
            table.Columns.Add("Priority", typeof(Int32));
            table.Columns.Add("Severity", typeof(string));
            table.Columns.Add("CreatedDate", typeof(DateTime));
            table.Columns.Add("ResolvedDate", typeof(DateTime));
            table.Columns.Add("ClosedDate", typeof(DateTime));
            table.Columns.Add("Tags", typeof(string));

            foreach (var metric in metricList)
            {
                var vso = metric.GetStringDetailValue("VSO");
                var project = metric.GetStringDetailValue("Project");
                var id = metric.GetIntegerDetailValue("Id");
                var title = metric.GetStringDetailValue("System.Title");
                var description = metric.GetStringDetailValue("Microsoft.VSTS.TCM.ReproSteps");
                var state = metric.GetStringDetailValue("System.State");
                var priority = metric.GetIntegerDetailValue("Microsoft.VSTS.Common.Priority");
                var severity = metric.GetStringDetailValue("Microsoft.VSTS.Common.Severity");
                var createdDate = metric.GetDateTimeDetailValue("System.CreatedDate");
                var resolvedDate = metric.GetDateTimeDetailValue("Microsoft.VSTS.Common.ResolvedDate");
                var closedDate = metric.GetDateTimeDetailValue("Microsoft.VSTS.Common.ClosedDate");
                var tags = metric.GetStringDetailValue("System.Tags");

                table.Rows.Add(vso, project, id, title, description, state, priority, severity, createdDate, resolvedDate, closedDate, tags);
            }

            DBUtil.ExecuteMetricMergeQuery(ConnectionString, "prc_MergeVSOWorkItem", "@newInfo", table);
        }
    }
}
