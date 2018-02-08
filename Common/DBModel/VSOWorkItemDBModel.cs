using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.DBModel
{
    public class VSOWorkItemDBModel
    {
        public VSOWorkItemDBModel(int id, Dictionary<string, object> fields)
        {
            this.Id = id;
            this.fields = fields;
        }

        public Dictionary<string, object> fields;
        public int Id;
    }
}
