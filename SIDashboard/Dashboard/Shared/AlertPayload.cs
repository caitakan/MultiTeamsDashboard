using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Shared
{
    public class AlertWebHookPayload
    {
        public string status { get; set; }
        public AlertContext context { get; set; }

        public Dictionary<string, string> properties { get; set; }
    }

    public class AlertContext
    {
        public DateTime timestamp { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string conditionType { get; set; }
        public string subscriptionId { get; set; }
        public string resourceGroupName { get; set; }
        public string resourceName { get; set; }
        public string resourceType { get; set; }
        public string resourceId { get; set; }
        public string resourceRegion { get; set; }
        public string portalLink { get; set; }
    }
}