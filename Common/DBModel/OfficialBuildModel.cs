using System;

namespace Microsoft.BusinessAI.Dashboard.Common.DBModel
{
    public class OfficialBuildModel
    {
        public OfficialBuildModel(string vso, string project, int id, bool result, string srcBranch, DateTime creationDate)
        {
            this.VSO = vso;
            this.Project = project;
            this.BuildId = id;
            this.Result = result;
            this.SourceBranch = srcBranch;
            this.CreationDate = creationDate;
        }

        public string VSO { get; set; }

        public string Project { get; set; }

        public int BuildId { get; set; }

        public bool Result { get; set; }

        public string SourceBranch { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
