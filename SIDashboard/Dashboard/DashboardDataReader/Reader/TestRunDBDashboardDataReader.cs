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
    public class TestRunDBDashboardDataReader : DBDashboardDataReader
    {
        public TestRunDBDashboardDataReader(string connectionString) : base(connectionString)
        {
        }        

        protected override MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number)
        {            
            return MetricSummaryAlertLevel.Pass;            
        }

        public override DashboardData read()
        {
            var dashboard = new DashboardData();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (SqlConnection conn = new SqlConnection(this.DBConnectionString))
            {

                dataAdapter.SelectCommand = new SqlCommand("prc_GetTestRunDashboard", conn);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    DataTable returnTables = dataSet.Tables[0];
                    foreach (DataRow row in returnTables.Rows)
                    {
                        dashboard.DateLabel = ReaderConstant.DateLabelName;
                        var testCaseNum = row["AvgTestCount"] != DBNull.Value ? Convert.ToDouble(row["AvgTestCount"]) : 0.0;

                        PopulateMetricNumber(dashboard, row[ReaderConstant.DateLabelName].ToString(), testCaseNum);
                    }
                }
                PopulateDashboardAlertLevel(dashboard);
                return dashboard;
            }
        }
    }
}