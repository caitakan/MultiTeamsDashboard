using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.DBModel
{
    public class ReleaseModel
    {
        public ReleaseModel(string vso, string project, int id, bool result, DateTime createdDate)
        {
            this.vso = vso;
            this.project = project;
            this.id = id;
            this.result = result;
            this.CreationDate = createdDate;
        }

        public string vso { get; set; }
        public string project { get; set; }
        public int id { get; set; }
        public bool result { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
