using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class TestRunUtil
    {
        private const string BUILD_TEST_RUN_QUERY_TEMPLATE = @"https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/test/runs?api-version=1.0&includeRunDetails=true&buildUri=vstfs:///Build/Build/{2}";
        private const string RELEASE_TEST_RUN_QUERY_TEMPLATE = @"https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/test/ResultSummaryByRelease?releaseEnvId={2}&releaseId={3}";

        /// <summary>
        /// Get Test Result from for a build
        /// </summary>
        public static List<TestRunModel> GetBuildTestRunDataModel(string vso, string project, int buildId, string token)
        {
            var url = string.Format(BUILD_TEST_RUN_QUERY_TEMPLATE, vso, project, buildId);
            var result = new List<TestRunModel>();
            var rawResponse = VSOApiUtil.GetResponse(url, token);
            var response = JsonConvert.DeserializeObject<TestRunListResponse>(rawResponse);

            if (response.value.Count > 0)
            {
                foreach (var item in response.value)
                {
                    var model = new TestRunModel(vso, project, buildId, item.id, item.passedTests, item.totalTests, item.startedDate);
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// Get Test Result from for a release
        /// </summary>
        public static TestRunModel GetReleaseTestRunDataModel(string vso, string project, string releaseEnvId, int releaseId, DateTime creationTime, string token)
        {
            var url = string.Format(RELEASE_TEST_RUN_QUERY_TEMPLATE, vso, project, releaseEnvId, releaseId);
            var rawResponse = VSOApiUtil.GetResponse(url, token);
            var response = JsonConvert.DeserializeObject<ResultSummaryResponse>(rawResponse);

            TestRunModel result = null;

            if (response != null && response.aggregatedResultsAnalysis != null && response.aggregatedResultsAnalysis.resultsByOutcome != null)
            {
                var outcome = response.aggregatedResultsAnalysis.resultsByOutcome;
                var failed = (outcome.Failed != null) ? outcome.Failed.count : 0;
                var passed = (outcome.Passed != null) ? outcome.Passed.count : 0;
                result = new TestRunModel(vso, project, releaseId, releaseId, passed, failed + passed, creationTime);
            }
            return result;
        }        
    }
}
