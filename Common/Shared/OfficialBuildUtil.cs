using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class OfficialBuildUtil
    {
        private const string OFFICIAL_BUILD_QUERY_WITH_PAGING_TEMPLATE =
                @"https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/build/builds?api-version=2.0&definitions={2}&minFinishTime={3}&maxFinishTime={4}&$top={5}";

        private static readonly HashSet<string> ValidOfficialBuildStatus =
                new HashSet<string>{
                    "completed"
                };

        private static readonly HashSet<string> ValidOfficialBuildResult =
            new HashSet<string>{
                    "succeeded",
                    "failed"
            };

        public static string GetQueryStringWithPaging(string vso, string project, string buildDefinitionIds, string minFinishTime, string maxFinishTime, int top = 1000, string status = "Completed")
        {
            return string.Format(OFFICIAL_BUILD_QUERY_WITH_PAGING_TEMPLATE, vso, project, buildDefinitionIds, minFinishTime, maxFinishTime, top);
        }

        public static List<OfficialBuildModel> GetOfficialBuildDBModel(string vso, string project, string buildListQuery, string token)
        {
            var result = new List<OfficialBuildModel>();

            //Get list of PR;
            var response = VSOApiUtil.GetResponse(buildListQuery, token);
            var buildListResponse = JsonConvert.DeserializeObject<OfficialBuildListResponse>(response);
            
            //For each PR, we get comment
            foreach (var build in buildListResponse.value)
            {
                var buildResult = build.result == "succeeded";
                var creationDate = DateTime.Parse(build.queueTime);
                result.Add(new OfficialBuildModel(vso, project, build.id, buildResult, build.sourceBranch, creationDate));
            }
            return result;
        }

        public static List<OfficialBuildModel> GetOfficialBuildDBModel(string vso, string project, string buildIdList, DateTime begin, DateTime end, string token)
        {
            var url = GetQueryStringWithPaging(vso,
                                    project,
                                    buildIdList,
                                    begin.ToString("yyyy-MM-dd"),
                                    end.ToString("yyyy-MM-dd"));
            return GetOfficialBuildDBModel(vso, project, url, token);

        }

        public static int GetMaxBuildId(DateTime begin, DateTime end, string vso, string project, string buildId, string token)
        {
            var url = string.Format(OFFICIAL_BUILD_QUERY_WITH_PAGING_TEMPLATE, vso, project, buildId, begin.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"), 1);
            var response = VSOApiUtil.GetResponse(url, token);
            var buildList = JsonConvert.DeserializeObject<OfficialBuildListResponse>(response);
            if (buildList.value != null && buildList.value.Count > 0)
            {
                return buildList.value[0].id;
            }
            return -1;
        }
    }
}
