using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Shared
{
    public class SalesIntelligenceWebApiConstant
    {

        public const string PING_KUSTO_REQUEST_BODY_TEMPLATE = @"{{""query"":""\n\r\navailabilityResults \r\n| where timestamp < now() \r\n|where name matches regex \""InsideSales-QueueScoringPing\""  \r\n| summarize OldSuccess=sumif(itemCount, success==1 and timestamp < datetime('{0}')), OldTotal = sumif(itemCount, timestamp < datetime('{0}')), \r\n    PrevSuccess=sumif(itemCount, success==1 and timestamp >= datetime('{0}') and timestamp < datetime('{1}')), PrevTotal = sumif(itemCount, timestamp >= datetime('{0}') and timestamp < datetime('{1}')),\r\n    CurrSuccess=sumif(itemCount, success==1 and timestamp >= datetime('{1}') and timestamp < datetime('{2}')), CurrTotal = sumif(itemCount, timestamp >= datetime('{1}') and timestamp < datetime('{2}'))\r\n\r\n"",""properties"":{{""Options"":{{""deferpartialqueryfailures"":true}}}}}}";
        public const string PING_SERVICE_UPTIME = "PING_SERVICE_UP";
        public const string PING_SERVICE_UPTIME_KUSTO_ENDPOINT = "https://api.applicationinsights.io/beta/apps/475c9ba7-c232-4db0-a808-d80f21fecdfc/query";

        public const string INSIDE_SALES_KUSTO_ENDPOINT = "https://api.applicationinsights.io/beta/apps/662416b3-6018-4a31-9dc3-79475ce2b894/query";

        public const string QUEUE_SCORING_KUSTO_REQUEST_BODY_TEMPLATE = @"{{""query"":""\nrequests\r\n| order by timestamp desc\r\n| where timestamp < now()\r\n| summarize OldSuccess=   sumif(itemCount, resultCode==200 and timestamp < datetime('{0}') ),  \r\n OldTotal=    sumif(itemCount, timestamp < datetime('{0}') ),  \r\n PrevSuccess= sumif(itemCount, resultCode==200 and timestamp >= datetime('{0}') and timestamp < datetime('{1}')) , \r\n PrevTotal=   sumif(itemCount, timestamp >= datetime('{0}') and timestamp < datetime('{1}')), \r\n CurrSuccess= sumif(itemCount, resultCode==200 and timestamp >= datetime('{1}') and timestamp < datetime('{2}')), \r\n CurrTotal = sumif(itemCount, timestamp >= datetime('{1}') and timestamp < datetime('{2}'))""}}";
        public const string QUEUE_SCORING_SERVICE_UPTIME = "QUEUE_SCORING_SERVICE_UPTIME";

        public const string LATENCY_KUSTO_REQUEST_BODY_TEMPLATE = @"{{""query"":""requests | where timestamp <= now() | summarize OldLatencyTotalDuration = sumif(duration * itemCount, timestamp < datetime('{0}')), OldOldLatencyCount = sumif(itemCount, timestamp < datetime('{0}')),  PrevLatencyTotalDuration = sumif(duration * itemCount, (timestamp >= datetime('{0}') and timestamp < datetime('{1}'))), PrevLatencyCount = sumif(itemCount, timestamp >= datetime('{0}') and timestamp < datetime('{1}')), CurrLatencyTotalDuration = sumif(duration * itemCount, ( timestamp >= datetime('{1}') and timestamp < datetime('{2}'))), CurrLatencyCount = sumif(itemCount, ( timestamp >= datetime('{1}') and timestamp < datetime('{2}')))""}}";
        public const string LATENCY = "LATENCY";
    }
}