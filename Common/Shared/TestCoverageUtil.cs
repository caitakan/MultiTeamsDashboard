using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class TestCoverageUtil
    {
        private const string TEST_COVERAGE_QUERY_TEMPLATE = @"https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/test/codeCoverage?api-version=2.0-preview.1&buildId={2}&flags=1";

        public static List<TestCoverageModel> GetTestCoverageForBuild(string vso, string project, int buildId, string token)
        {
            var result = new List<TestCoverageModel>();
            var url = string.Format(TEST_COVERAGE_QUERY_TEMPLATE, vso, project, buildId);
            var response = VSOApiUtil.GetResponse(url, token);
            var coverage = JsonConvert.DeserializeObject<TestCoverageListResponse>(response);

            if (coverage.count > 0)
            {
                var moduleList = coverage.value[0].modules;
                foreach (var module in moduleList)
                {
                    var stat = new TestCoverageModel(vso, project, buildId, module.name, module.statistics.linesCovered, module.statistics.linesNotCovered);
                    result.Add(stat);
                }
            }
            return result;
        }
    }
}
