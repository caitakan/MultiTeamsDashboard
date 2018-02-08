using System;

namespace Microsoft.BusinessAI.Dashboard.Common.DBModel
{
    public class PullRequestModel
    {
        public PullRequestModel(string vso, string project, string repo, int id, int count, DateTime creationDate)
        {
            this.VSO = vso;
            this.Project = project;
            this.Repository = repo;
            this.PRId = id;
            this.CountOfCommentNotFixed = count;
            this.CreationDate = creationDate;
        }

        public string VSO { get; set; }
        
        public string Project { get; set; }

        public string Repository { get; set; }

        public int PRId { get; set; }

        public int CountOfCommentNotFixed { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
