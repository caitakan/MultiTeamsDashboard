using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public class VSOWorkItemCollector : WorkItemCollector
    {
        public VSOWorkItemCollector(IMetricStorageWriter writer, SolutionConfig config)
            : base(writer, config)
        {
        }

        public override void CollectWorkItem(string vso, string project, string areaPath, DateTime start, DateTime end, string token)
        {
            var workItemErrorList = WorkItemUtil.GetVSOWorkItemDBModel(vso, start, end, token, areaPath);
            var metricList = new List<Metric>();

            foreach (var item in workItemErrorList)
            {
                var detail = item.fields;
                detail["VSO"] = vso;
                detail["Project"] = project;
                detail["Id"] = item.Id;

                var metric = new Metric("VSO WorkItem", MetricType.VsoWorkItem, detail);
                metricList.Add(metric);
            }

            writer.Write(metricList);
        }
    }
}


