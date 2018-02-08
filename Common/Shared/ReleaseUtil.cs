using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class ReleaseUtil
    {
        private const string RELEASE_LIST_QUERY_WITH_DATE_RANGE_TEMPLATE =
                "https://{0}.vsrm.visualstudio.com/DefaultCollection/{1}/_apis/release/releases?definitionId={2}&minCreatedTime={3}&maxCreatedTime={4}";

        private const string RELEASE_DETAIL_QUERY_TEMPLATE =
                "https://{0}.vsrm.visualstudio.com/DefaultCollection/{1}/_apis/release/releases/{2}";

        private const string MAX_RELEASE_ID_QUERY_TEMPLATE = RELEASE_LIST_QUERY_WITH_DATE_RANGE_TEMPLATE + "&$top=1";

        public static string GetQueryStringWithDateRange(string vso, string project, int definitionId, DateTime start, DateTime end)
        {
            return string.Format(RELEASE_LIST_QUERY_WITH_DATE_RANGE_TEMPLATE, vso, project, definitionId, start, end);
        }

        public static List<ReleaseModel> GetReleaseDBModel(string vso, string project, string releaseQuery, string token)
        {
            var result = new List<ReleaseModel>();

            //Get list of PR;
            var rawResponse = VSOApiUtil.GetResponse(releaseQuery, token);
            var response = JsonConvert.DeserializeObject<ReleaseListResponse>(rawResponse);

            //For each PR, we get comment
            foreach (var release in response.value)
            {
                result.Add(new ReleaseModel(vso, project, release.id, GetReleaseStatus(vso, project, release.id, token), release.createdOn));
            }
            return result;
        }

        public static bool GetReleaseStatus(string vso, string project, int id, string token)
        {
            var query = string.Format(RELEASE_DETAIL_QUERY_TEMPLATE, vso, project, id);

            var rawResponse = VSOApiUtil.GetResponse(query, token);
            var release = JsonConvert.DeserializeObject<ReleaseResponse>(rawResponse);

            var failureCount = release.environments.Count(env => env.status != "succeeded");

            return failureCount <= 0;
        }

        public static List<ReleaseModel> GetReleaseByReleaseDefinitionId(string vso, string project, DateTime start, DateTime end, string releaseDefinitionId, string token)
        {
            var query = string.Format(RELEASE_LIST_QUERY_WITH_DATE_RANGE_TEMPLATE, vso, project, releaseDefinitionId, start, end);
            return GetReleaseDBModel(vso, project, query, token);
        }
    }
}
