using Dashboard.Models;
using Dashboard.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dashboard.DashboardDataReader
{
    public abstract class DBDashboardDataReader : IDashboardDataReader
    {
        private string connectionString = null;

        protected string DBConnectionString
        {
            get { return this.connectionString; }
        }

        public DBDashboardDataReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public abstract DashboardData read();

        protected void PopulateDashboardAlertLevel(DashboardData data)
        {
            data.CurrentMonthAlertLevel = GetMetricSummaryAlertLevel(data.CurrMonthNumber);
            data.PreviousMonthAlertLevel = GetMetricSummaryAlertLevel(data.PrevMonthNumber);
            data.OlderMonthAlertLevel = GetMetricSummaryAlertLevel(data.OlderMonthNumber);
        }

        protected virtual MetricSummaryAlertLevel GetMetricSummaryAlertLevel(double number)
        {
            return MetricSummaryAlertLevel.NotApplicable;
        }

        protected void PopulateMetricNumber(DashboardData dashboard, string label, double number)
        {
            switch (label)
            {
                case ReaderConstant.OlderMonthLabel:
                    {
                        dashboard.OlderMonthNumber = number;
                    }
                    break;
                case ReaderConstant.CurrMonthLabel:
                    {
                        dashboard.CurrMonthNumber = number;
                    }
                    break;
                case ReaderConstant.PrevMonthLabel:
                    {
                        dashboard.PrevMonthNumber = number;
                    }
                    break;
                default:
                    {
                        throw new Exception("ERROR: Unknown month label");
                    }
            }
        }

        protected DashboardData ReadSingleColumnTable(string proc, string columnName)
        {
            var dashboard = new DashboardData();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (SqlConnection conn = new SqlConnection(this.DBConnectionString))
            {

                dataAdapter.SelectCommand = new SqlCommand(proc, conn);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    DataTable returnTables = dataSet.Tables[0];
                    foreach (DataRow row in returnTables.Rows)
                    {
                        dashboard.DateLabel = ReaderConstant.DateLabelName;
                        var valueRaw = row[columnName];
                        var value = (valueRaw != DBNull.Value) ? Convert.ToDouble(valueRaw) : 0.0;

                        PopulateMetricNumber(dashboard, row[ReaderConstant.DateLabelName].ToString(), (double)value);
                    }
                }
                PopulateDashboardAlertLevel(dashboard);
                return dashboard;
            }
        }
    }
}