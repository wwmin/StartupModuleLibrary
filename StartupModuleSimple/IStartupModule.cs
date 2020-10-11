using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModuleSimple
{
    public interface IStartupModule
    {
        void ConfigureServices(IServiceCollection services, ConfigureServicesContext context);

        void Configure(IApplicationBuilder app, ConfigureServicesContext context);
    }
}
