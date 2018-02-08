using Microsoft.BusinessAI.Dashboard.Common.DBModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class DBUtil
    {
        public static bool ExecuteQuery(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    // Check Error result < 0 means faild
                    return result >= 0;
                }
            }
        }

        public static bool ExecuteMetricMergeQuery(string connectionString, string procName, string parameterName, DataTable newMetricData)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(procName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                //Pass table Valued parameter to Store Procedure
                SqlParameter sqlParam = cmd.Parameters.AddWithValue(parameterName, newMetricData);
                sqlParam.SqlDbType = SqlDbType.Structured;
                var result = cmd.ExecuteNonQuery();
                return result >= 0;
            }
        }

        public static PullRequestModel GetPullRequest(string connectionString, string vso, string project, int id)
        {
            var list = new List<PullRequestModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT * FROM dbo.PullRequest WHERE VSO = '{0}' AND Project = '{1}' AND PRId = {2};", vso, project, id);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PullRequestModel(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetDateTime(5)));
                        }
                        if (list.Count > 1)
                        {
                            throw new Exception("Error: should return max 1 record");
                        }
                    }
                }
            }

            return list[0];
        }

        public static OfficialBuildModel GetOfficialBuildModel(string connectionString, string vso, string project, int id)
        {
            var list = new List<OfficialBuildModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT * FROM dbo.OfficialBuild WHERE VSO = '{0}' AND Project = '{1}' AND BuildId = {2};", vso, project, id);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new OfficialBuildModel(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetBoolean(3), reader.GetString(4), reader.GetDateTime(5)));
                        }
                        if (list.Count > 1)
                        {
                            throw new Exception("Error: should return max 1 record");
                        }
                    }
                }
            }
            return list[0];
        }

        public static TestCoverageModel GetTestCoverage(string connectionString, string vso, string project, int id)
        {
            var list = new List<TestCoverageModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT * FROM dbo.TestCoverage WHERE VSO = '{0}' AND Project = '{1}' AND BuildId = {2};", vso, project, id);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TestCoverageModel(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5)));
                        }
                        if (list.Count > 1)
                        {
                            throw new Exception("Error: should return max 1 record");
                        }
                    }
                }
            }

            return list[0];
        }

        public static VSOWorkItemDBModel GetAlert(string connectionString, int id)
        {
            var list = new List<VSOWorkItemDBModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT Id, Title FROM dbo.VSOWorkItem WHERE Id = {0};", id);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dict = new Dictionary<string, object>();
                            dict["Title"] = reader.GetString(1);
                            var model = new VSOWorkItemDBModel(reader.GetInt32(0), dict);
                            list.Add(model);
                        }
                        if (list.Count > 1)
                        {
                            throw new Exception("Error: should return max 1 record");
                        }
                    }
                }
            }

            return list[0];
        }

        public static TestRunModel GetTestRun(string connectionString, int id)
        {
            var list = new List<TestRunModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT * FROM dbo.TestRun WHERE RunId = {0};", id);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dict = new Dictionary<string, object>();
                            var model = new TestRunModel(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDateTime(6));
                            list.Add(model);
                        }
                        if (list.Count > 1)
                        {
                            throw new Exception("Error: should return max 1 record");
                        }
                    }
                }
            }

            return list[0];
        }

    }
}
