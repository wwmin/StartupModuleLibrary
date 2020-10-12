using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StartupModules.Web.Jobs
{
    public class MyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
             {
                 var now = DateTime.Now;
                 Console.WriteLine($"Hello Quartz! Time {now.ToShortDateString()} {now.ToLongTimeString()}");
                 Thread.Sleep(1000 * 2);
                 Console.WriteLine($"JobDetail的组和名字:{context.JobDetail.Key},{context.JobDetail.Description}");
                 Console.WriteLine();

             });
        }

        public static IJobDetail GetJobDetail()
        {
            return JobBuilder.Create<MyJob>()
                .WithIdentity("test.job")
                .WithDescription("任务描述:test.job")
                .Build();
        }

        public static ITrigger GetTrigger()
        {
            return TriggerBuilder.Create()
                .StartNow()
                .ForJob(GetJobDetail())
                .WithPriority(10)
                .WithIdentity("test.job.1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(3).WithRepeatCount(-1).Build())
                .Build();
        }
    }
}
