using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.YTasks.Jobs
{
    public class GameOrdersWithParmsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string content = dataMap.GetString("jobData");
            Console.WriteLine(content + "-" + DateTime.Now);


            return null;
        }

    }
}
