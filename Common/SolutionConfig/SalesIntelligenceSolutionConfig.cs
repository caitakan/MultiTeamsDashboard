using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common
{
    public class SalesIntelligenceSolutionConfig : SolutionConfig
    {
        private Dictionary<string, object> variables = new Dictionary<string, object>()
                    {
                        {SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR,"Server=tcp:bizdashboard.database.windows.net,1433;Initial Catalog=BizDashboard;Persist Security Info=False;User ID=wusun;Password=Passw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"},
                        {SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV , "Server=tcp:bizdashboard.database.windows.net,1433;Initial Catalog=BizDashboard-Dev;Persist Security Info=False;User ID=wusun;Password=Passw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"},
                        {SolutionConfigName.PERSONAL_ACCESS_TOKEN , "f6hd76jtpdud36zgq7rb4kryuoknfssp7ssbyqb43afaed6q4aka"},
                        {SolutionConfigName.VSO_NAME , "bizai"},
                        {SolutionConfigName.VSO_PROJECT_NAME , "BizAI"},
                        {SolutionConfigName.OFFICIAL_BUILD_ID , "2,14,15"},
                        {SolutionConfigName.DASHBOARD_APP_INSIGHT_KEY , "d75a7bce-088a-45a2-b6dc-8473a9ebc8aa"},
                        {SolutionConfigName.PULL_REQUEST_REPOSITORIES , new string[] { "SalesIntelligence" }},
                        {SolutionConfigName.FUNCTIONAL_TEST_RELEASE_DEFINITION_ID , "8"},
                        {SolutionConfigName.FUNCTIONAL_TEST_RELEASE_ENV_ID , "0"},
                        {SolutionConfigName.INCIDENT_VSO_AREA_PATH , "BizAI\\\\SI\\\\Engineering\\\\Operations\\\\Incidents"}
                    };


        public override Dictionary<string, object> Variables
        {
            get { return variables; }
        }
    }
}
