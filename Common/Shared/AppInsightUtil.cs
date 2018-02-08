using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class AppInsightUtil
    {
        private static TelemetryClient sTelemetryClient = new TelemetryClient();

        public static void Initialize(string appInsightKey)
        {
            TelemetryConfiguration.Active.InstrumentationKey = appInsightKey;
        }

        public static void StartPingThread()
        {
            sTelemetryClient.Context.User.Id = Environment.UserName;
            sTelemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
            sTelemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += PingQueueScoringAPI;
            timer.Start();
        }

        private static void PingQueueScoringAPI(object source, System.Timers.ElapsedEventArgs e)
        {
            sTelemetryClient.TrackAvailability("QueueScoringHello", new DateTimeOffset(DateTime.Now), TimeSpan.FromSeconds(2.0), "US", true);
        }
    }
}
