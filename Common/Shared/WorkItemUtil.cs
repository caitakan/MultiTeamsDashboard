using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using Microsoft.BusinessAI.Dashboard.Common.MetricVSOResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    /// <summary>
    /// This class is handles all VSO REST api related logic for WorkItems, including bug, task ...
    /// All VSO work items related metric collector class will reference it
    /// </summary>
    public class WorkItemUtil
    {
        private const string VSO_WORK_ITEM_LIST_QUERY_TEMPLATE =
            @"{{""query"": ""Select [System.Id] 
                             From WorkItems 
                             Where [System.WorkItemType] = 'Bug' 
                             And ( [System.AreaPath] = '{2}' )
                             And ( [System.Tags] Contains 'Customer Incident' OR [System.Tags] Contains 'Critical Error' OR [System.Tags] Contains 'Out of SLA' )  
                             And ( [System.CreatedDate] >= '{0}' And [System.CreatedDate] < '{1}') ""}}";

        private const string VSO_WORK_ITEM_INSTANCE_QUERY_TEMPLATE = @"https://{0}.visualstudio.com/DefaultCollection/_apis/wit/workitems/{1}?api-version=1.0";

        private const string VSO_WIQL_END_POINT_TEMPLATE = @"https://{0}.visualstudio.com/DefaultCollection/_apis/wit/wiql?api-version=1.0";

        public static List<VSOWorkItemDBModel> GetVSOWorkItemDBModel(string vso, DateTime start, DateTime end, string token, string areaPath)
        {
            var result = new List<VSOWorkItemDBModel>();
            var url = string.Format(VSO_WIQL_END_POINT_TEMPLATE, vso);
            var alertListQuery = string.Format(VSO_WORK_ITEM_LIST_QUERY_TEMPLATE, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"), areaPath);

            //Get list of Alert Error from VSO API
            var response = GetWIQLResponse(url, token, alertListQuery);
            var workItemListResponse = JsonConvert.DeserializeObject<VSOWorkItemListResponse>(response);
            foreach (var workItem in workItemListResponse.workItems)
            {
                var witQuery = string.Format(VSO_WORK_ITEM_INSTANCE_QUERY_TEMPLATE, vso, workItem.id);
                var workItemResponseStr = VSOApiUtil.GetResponse(witQuery, token);
                var workItemResponse = JsonConvert.DeserializeObject<VSOResponse>(workItemResponseStr);
                var model = new VSOWorkItemDBModel(workItem.id, workItemResponse.fields);
                result.Add(model);
            }

            return result;
        }

        public static string GetWIQLResponse(string url, string token, string json)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", token))));

                //serialize the wiql object into a json string   
                var method = new HttpMethod("POST");
                var request = new HttpRequestMessage(method, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
        }
    }
}
