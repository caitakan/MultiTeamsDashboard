using Microsoft.BusinessAI.Dashboard.Common;
using System;
using System.Collections.Generic;

namespace Microsoft.BusinessAI.Dashboard.Common
{
    public abstract class SolutionConfig
    {
        public abstract Dictionary<string, object> Variables { get; }

        public void OverrideDashboardDBConnection(string connection)
        {
            Variables[SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR] = connection;
        }

        public object this[string keyName]
        {
            get
            {
                return Variables[keyName];
            }
        }

    }

    public class SolutionConfigManager
    {
        private static Dictionary<SolutionName, SolutionConfig> mapping = new Dictionary<SolutionName, SolutionConfig>() {
            {SolutionName.SalesIntelligence, new SalesIntelligenceSolutionConfig() },
            {SolutionName.CustomerCareIntelligence, new CustomerCareIntelligenceSolutionConfig() }
        };

        public static SolutionConfig GetSolutionConfig(SolutionName solution)
        {
            return mapping[solution];
        }
    }
}




