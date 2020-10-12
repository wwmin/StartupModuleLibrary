using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using StartupModules.Web.Jobs;
using StartupModuleSimple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModules.Web
{
    public class QuartzStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            Console.WriteLine("HangfireStartupModule --- ConfigureServices");
            if (context.HostEnvironment.IsDevelopment())
            {
                Console.WriteLine("isDevelopment");
            }
            context.Options.Settings.TryGetValue("AddHangfire", out object isQuartz);
            if (isQuartz != null && (bool)isQuartz)
            {
                //services.AddHangfireContrib(c => c.UseMemoryStorage());
                Console.WriteLine("Hello Quartz! Main");

                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = factory.GetScheduler().Result;

                //将job和trigger注册到scheduler中
                scheduler.ScheduleJob(MyJob.GetJobDetail(), MyJob.GetTrigger());

                //start让调度线程启动[调度线程可以从job store中获取快要执行的trigger,然后获取trigger关联的job,执行job]
                scheduler.Start();
            }
        }

        public void Configure(IApplicationBuilder app, ConfigureServicesContext context)
        {
            Console.WriteLine("HangfireStartupModule --- Configure");
        }
    }
}
