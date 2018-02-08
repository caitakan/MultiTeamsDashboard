using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse
{
    public class Metric
    {
        public Metric(string Name, MetricType type, Dictionary<string, object> detail)
        {
            this.Name = Name;
            this.Type = type;
            this.Detail = detail;
        }

        public string Name { get; }
        public MetricType Type { get; }

        Dictionary<string, object> Detail;

        public object GetDetail(string detailName)
        {
            return Detail[detailName];
        }

        public int? GetIntegerDetailValue(string key)
        {
            int? nullValue = null;
            return Detail.ContainsKey(key) ? Convert.ToInt32(Detail[key]) : nullValue;
        }

        public string GetStringDetailValue(string key)
        {
            return Detail.ContainsKey(key) ? Detail[key].ToString() : null;
        }

        public DateTime? GetDateTimeDetailValue(string key)
        {
            DateTime? nullValue = null;
            return Detail.ContainsKey(key) ? DateTime.Parse(Detail[key].ToString()) : nullValue;
        }
    }

}
