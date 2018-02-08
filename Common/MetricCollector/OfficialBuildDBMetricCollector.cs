using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Microsoft.BusinessAI.Dashboard.Common.MetricStorageWriter;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.MetricCollector
{
    public class OfficialBuildDBMetricCollector : BaseMetricCollector
    {
        public OfficialBuildDBMetricCollector(IMetricStorageWriter writer, SolutionConfig config)
            : base(writer, config)
        {

        }

        public override void CollectMetric()
        {
            var now = DateTime.Now;
            var nowDate = now.AddDays(1);
            var start = now.AddDays(-5).Date; //This is to let code recover all data automatically if any error occurs.
            const int BUILD_PER_REQUEST = 1000;
            this.CollectBuildData(start, nowDate, BUILD_PER_REQUEST);
        }

        public void CollectBuildData(DateTime minFinishTime, DateTime maxFinishTime, int BUILD_PER_REQUEST)
        {
            var vso = this.config[SolutionConfigName.VSO_NAME].ToString();
            var project = this.config[SolutionConfigName.VSO_PROJECT_NAME].ToString();
            var token = this.config[SolutionConfigName.PERSONAL_ACCESS_TOKEN].ToString();
            var query = OfficialBuildUtil.GetQueryStringWithPaging(
                vso,
                project,
                this.config[SolutionConfigName.OFFICIAL_BUILD_ID].ToString(),
                minFinishTime.ToString(),
                maxFinishTime.ToString(),
                BUILD_PER_REQUEST,
                "Completed"
            );

            var buildFromVSO = OfficialBuildUtil.GetOfficialBuildDBModel(vso, project, query, token);
            var metricList = new List<Metric>();
            foreach (var build in buildFromVSO)
            {
                var detail = new Dictionary<string, object>();
                detail["VSO"] = build.VSO;
                detail["Project"] = build.Project;
                detail["BuildId"] = build.BuildId;
                detail["Result"] = build.Result ? 1 : 0;
                detail["SourceBranch"] = build.SourceBranch;
                detail["CreationDate"] = build.CreationDate;
                metricList.Add(new Metric("OfficialBuild", MetricType.OfficialBuild, detail));
            }

            writer.Write(metricList);
        }
    }
}
