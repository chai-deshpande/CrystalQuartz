using System.Collections.Generic;

namespace CrystalQuartz.Core
{
    using Domain;

    /// <summary>
    /// Translates Quartz.NET entyties to CrystalQuartz objects graph.
    /// </summary>
    public interface ISchedulerDataProvider
    {
        SchedulerData Data { get; }

        JobDetailsData GetJobDetailsData(string name, string group);

        IEnumerable<JobLogData> GetJobLogs(string jobName, string groupName);
    }
}
