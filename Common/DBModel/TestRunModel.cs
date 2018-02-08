using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.DBModel
{
    public class TestRunModel
    {
        public TestRunModel(string vso, string project, int buildOrReleaseId, int runId, int passedTestNum, int totalTestNum, DateTime creationDate)
        {
            this.VSO = vso;
            this.Project = project;
            this.BuildOrReleaseId = buildOrReleaseId;
            this.RunId = runId;
            this.PassedTestNum = passedTestNum;
            this.TotalTestNum = totalTestNum;
            this.CreationDate = creationDate;
        }

        public string VSO { get; set; }
        public string Project { get; set; }
        public int BuildOrReleaseId { get; set; }
        public int RunId { get; set; }
        public int PassedTestNum { get; set; }
        public int TotalTestNum { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
