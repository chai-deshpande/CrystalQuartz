using System.Collections.Generic;
using CrystalQuartz.Core.Domain;

namespace CrystalQuartz.Core
{
    public interface IJobLogProvider
    {
        IEnumerable<JobLogData> GetJobLogs(string jobName, string jobGroup);
    }
}