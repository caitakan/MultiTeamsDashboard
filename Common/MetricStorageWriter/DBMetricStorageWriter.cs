using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter
{
    abstract public class DBMetricStorageWriter : IMetricStorageWriter
    {
        private string connectionString = null;
        public DBMetricStorageWriter(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Write(Metric metric)
        {
            var metricList = new List<Metric>();
            metricList.Add(metric);
            Write(metricList);
        }

        protected string ConnectionString
        {
            get { return this.connectionString; }
        }

        public abstract void Write(List<Metric> metricList);
    }
}
