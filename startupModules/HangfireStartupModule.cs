using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webenable.Hangfire.Contrib;

namespace startupModules
{
    public class HangfireStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                // Do something based on this
            }

            if ((bool)context.Options.Settings["AddHangfire"] == true)
            {
                services.AddHangfireContrib(c => c.UseMemoryStorage());
            }
        }

        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context) { }
    }
}
