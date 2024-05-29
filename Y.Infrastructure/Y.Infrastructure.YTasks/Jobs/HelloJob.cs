using Quartz;
using System;
using System.Threading.Tasks;

namespace Y.Infrastructure.YTasks.Jobs
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {

            Console.WriteLine(DateTime.Now);

            return null;
        }
    }
}
