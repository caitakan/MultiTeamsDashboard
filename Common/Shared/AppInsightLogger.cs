using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class AppInsightLogger
    {
        //Singleton stuff
        private static AppInsightLogger _instance = new AppInsightLogger();
        private TelemetryClient telemetry = new TelemetryClient();

        public static void Initialize(string appInsightKey)
        {
            TelemetryConfiguration.Active.InstrumentationKey = appInsightKey;
        }

        public static AppInsightLogger Instance
        {
            get { return _instance; }
        }

        public void Error(Exception exception)
        {
            this.telemetry.TrackException(exception);
        }

        public void TraceIteration()
        {
            this.telemetry.TrackTrace("LoopIteration at " + (DateTime.Now).ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
