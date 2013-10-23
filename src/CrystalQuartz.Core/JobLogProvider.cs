using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrystalQuartz.Core.Domain;

namespace CrystalQuartz.Core
{
    public class JobLogProvider : IJobLogProvider, IDisposable
    {
        protected SqlConnection Connection;

        public JobLogProvider()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Environment.MachineName].ToString());
        }

        protected void OpenConnection()
        {
            if (Connection == null)
            {
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings[Environment.MachineName].ToString());
            }

            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                return;
            }

            Connection.Open();
        }

        protected void CloseConnection()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public IEnumerable<JobLogData> GetJobLogs(string jobName, string jobGroup)
        {
            var jobLogs = new List<JobLogData>();
            var selectCommand = new SqlCommand
            {
                CommandText = "p_Job_ExecutionLog_Select",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };

            selectCommand.Parameters.AddWithValue("@jobName", jobName);

            try
            {
                OpenConnection();

                using (IDataReader dataReader = selectCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        jobLogs.Add(new JobLogData { Message = dataReader["LogMessage"].ToString(), TimeStamp = Convert.ToDateTime(dataReader["TimeStamp"]) });
                    }
                }
            }
            finally
            {
                CloseConnection();
            }

            return jobLogs;
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}