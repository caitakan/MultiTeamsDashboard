using System;

namespace Microsoft.BusinessAI.Dashboard.Common.DBModel
{
    public class TestCoverageModel
    {
        public TestCoverageModel(string vso, string project, int id, string moduleName, int linesCovered, int linesNotCovered)
        {
            this.VSO = vso;
            this.Project = project;
            this.BuildId = id;
            this.ModuleName = moduleName;
            this.LinesCovered = linesCovered;
            this.LinesNotCovered = linesNotCovered;
        }

        public string VSO { get; set; }

        public string Project { get; set; }

        public int BuildId { get; set; }

        public string ModuleName { get; set; }

        public int LinesCovered { get; set; }

        public int LinesNotCovered { get; set; }
    }
}
