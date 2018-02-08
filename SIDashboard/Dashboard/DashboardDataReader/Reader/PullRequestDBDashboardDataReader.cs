using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Models;
using System.Data.SqlClient;
using System.Data;
using Dashboard.Shared;

namespace Dashboard.DashboardDataReader
{
    public class PullRequestDBDashboardDataReader : DBDashboardDataReader
    {
        public PullRequestDBDashboardDataReader(string connectionString) : base(connectionString)
        {

        }

        protected override MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number)
        {
            const double PASS_NUMBER = 1.0;
            const double WARNING_NUMBER = 0.98;

            if (number == PASS_NUMBER)
            {
                return MetricSummaryAlertLevel.Pass;
            }
            else if (number > WARNING_NUMBER)
            {
                return MetricSummaryAlertLevel.Warning;
            }
            else
            {
                return MetricSummaryAlertLevel.Fail;
            }
        }

        public override DashboardData read()
        {
            var dashboard = new DashboardData();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (SqlConnection conn = new SqlConnection(this.DBConnectionString))
            {

                dataAdapter.SelectCommand = new SqlCommand("prc_GetPullRequestDashboard", conn) {
                    CommandType = CommandType.StoredProcedure
                };

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    DataTable returnTables = dataSet.Tables[0];
                    foreach (DataRow row in returnTables.Rows)
                    {
                        dashboard.DateLabel = ReaderConstant.DateLabelName;
                        var goodPRCount = row["GoodPRCount"] != DBNull.Value ? Convert.ToDouble(row["GoodPRCount"]) : 0.0;
                        var allPRCount = row["AllPRCount"] != DBNull.Value ? Convert.ToDouble(row["AllPRCount"]) : 0.0;
                        var ratio = (allPRCount == 0) ? 0.0 : goodPRCount / allPRCount;

                        PopulateMetricNumber(dashboard, row[ReaderConstant.DateLabelName].ToString(), ratio);
                    }
                }
                PopulateDashboardAlertLevel(dashboard);
                return dashboard;
            }
        }
    }
}