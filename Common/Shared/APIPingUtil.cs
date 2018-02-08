using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class APIPingUtil
    {
        private static TelemetryClient sInsideSalesPingClient = new TelemetryClient();
        private static System.Timers.Timer sInsideSalesPingTimer = new System.Timers.Timer(1000 * 60 * 2);

        public static string GetAuthAccessToken(string authority, string appId, string secret, string tokenEndPoint)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCredential = new ClientCredential(appId, secret);
            var access_token = authContext.AcquireTokenAsync(tokenEndPoint, clientCredential).Result.AccessToken;
            return access_token;
        }

        public static bool PingAPI(string authority, string appId, string secret, string tokenEndPoint, string api)
        {
            var access_token = GetAuthAccessToken(authority, appId, secret, tokenEndPoint);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                using (HttpResponseMessage response = client.GetAsync(api).Result)
                {
                    return (response.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        public static void PingScoringServiceDummyAPI(object source, System.Timers.ElapsedEventArgs e)
        {
            const string INSIDE_SALES_API_AUTHORITY = "https://login.microsoftonline.com/microsoft.onmicrosoft.com/";
            const string INSIDE_SALES_APP_ID = "e943af1b-8f91-49f8-a1a1-da92a1e08381";
            const string MS_SECRET = "UyFG10BQKWmz7FFY2V0WhxyFGfaatmbHMmIi44UT6kA=";
            const string INSIDE_SALES_TOKEN_URI = "https://72f988bf-86f1-41af-91ab-2d7cd011db47/ApiBackend-SalesIntelligenceProd";
            const string INSIDE_SALES_QUEUE_SCORING_DUMMY_API_URI = "https://apirgtykaganlm6o.salesintelligence.trafficmanager.net/api/v1";

            var pingStatus = PingAPI(INSIDE_SALES_API_AUTHORITY,
                                INSIDE_SALES_APP_ID,
                                MS_SECRET,
                                INSIDE_SALES_TOKEN_URI,
                                INSIDE_SALES_QUEUE_SCORING_DUMMY_API_URI);

            sInsideSalesPingClient.TrackAvailability("InsideSales-QueueScoringPing", new DateTimeOffset(DateTime.Now), TimeSpan.FromSeconds(0), "US", true);
        }

        public static void LanuchPingThread(string appInsightKey)
        {
            TelemetryConfiguration.Active.InstrumentationKey = appInsightKey;

            sInsideSalesPingClient.Context.User.Id = Environment.UserName;
            sInsideSalesPingClient.Context.Session.Id = Guid.NewGuid().ToString();
            sInsideSalesPingClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            sInsideSalesPingTimer.Elapsed += PingScoringServiceDummyAPI;
            sInsideSalesPingTimer.Start();
        }
    }
}
