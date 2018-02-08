using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class PullRequestUtil
    {
        private const string PR_QUERY_WITH_PAGING_TEMPLATE =
                @"https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/git/repositories/{2}/pullRequests?$top={3}&$skip={4}&status={5}";

        private const string PR_THREAD_QUERY_TEMPLATE =
                @"https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/git/repositories/{2}/pullRequests/{3}/threads?api-version=3.0-preview";

        private static readonly HashSet<string> ValidPullRequestCommentStatus =
                new HashSet<string>{
                    "closed",
                    "fixed",
                    "resolved",
                    "wontfix"
                };

        public static string GetQueryStringWithPaging(string vso, string project, string repoId, int top, int skip, string status = "Completed")
        {
            return string.Format(PR_QUERY_WITH_PAGING_TEMPLATE, vso, project, repoId, top, skip, status);
        }

        public static List<PullRequestCommentResponse> GetPullRequestCommentResponse(string query, string token)
        {
            var response = VSOApiUtil.GetResponse(query, token);

            //Get list of PR
            var pullReqeustCommentListResponse = JsonConvert.DeserializeObject<PullRequestCommentListResponse>(response);
            return pullReqeustCommentListResponse.value;
        }

        public static List<PullRequestModel> GetPullRequestDBModel(string vso, string project, string prListQuery, string repo, string token)
        {
            var result = new List<PullRequestModel>();

            //Get list of PR;
            var response = VSOApiUtil.GetResponse(prListQuery, token);
            var pullReqeustListResponse = JsonConvert.DeserializeObject<PullRequestListResponse>(response);

            //For each PR, we get comment
            foreach (var pr in pullReqeustListResponse.value)
            {
                int numOfUnresolvedComment = GetNumOfUnresolvedComment(pr.pullRequestId, repo, vso, project, token);
                var creationDate = DateTime.Parse(pr.creationDate);
                result.Add(new PullRequestModel(vso, project, repo, pr.pullRequestId, numOfUnresolvedComment, creationDate));

            }
            return result;
        }

        public static int GetNumOfUnresolvedComment(int prId, string repo, string vso, string project, string token)
        {
            var commentQuery = string.Format(PR_THREAD_QUERY_TEMPLATE, vso, project, repo, prId);

            int numOfUnresolvedComment = 0;
            var prCommentResponse = GetPullRequestCommentResponse(commentQuery, token);
            foreach (var comment in prCommentResponse)
            {
                if (comment.status != null && comment.isDeleted == false
                        && (!ValidPullRequestCommentStatus.Contains(comment.status.ToLower())))
                {
                    numOfUnresolvedComment++;
                }
            }
            return numOfUnresolvedComment;
        }
    }
}
