using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BusinessAI.Dashboard.Common
{
    public class CustomerCareIntelligenceSolutionConfig : SolutionConfig
    {
        private Dictionary<string, object> variables = new Dictionary<string, object>()
                    {
                        {SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR,"Server=tcp:bizdashboard.database.windows.net,1433;Initial Catalog=BizDashboard;Persist Security Info=False;User ID=wusun;Password=Passw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"},
                        {SolutionConfigName.BIZ_DASHBOARD_DB_CONNECTION_STR_DEV , "Server=tcp:bizdashboard.database.windows.net,1433;Initial Catalog=BizDashboard-Dev;Persist Security Info=False;User ID=wusun;Password=Passw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"},
                        {SolutionConfigName.PERSONAL_ACCESS_TOKEN , "f6hd76jtpdud36zgq7rb4kryuoknfssp7ssbyqb43afaed6q4aka"},
                        {SolutionConfigName.VSO_NAME , "bizai"},
                        {SolutionConfigName.VSO_PROJECT_NAME , "BizAI"},
                        {SolutionConfigName.OFFICIAL_BUILD_ID , "2,14,15"},
                        {SolutionConfigName.PULL_REQUEST_REPOSITORIES , new string[] { "SalesIntelligence" }}
                    };


        public override Dictionary<string, object> Variables
        {
            get { return variables; }
        }
    }
}
