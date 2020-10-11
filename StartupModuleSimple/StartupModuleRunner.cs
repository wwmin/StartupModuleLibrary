using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace StartupModuleSimple
{
    public class StartupModuleRunner
    {
        private readonly StartupModulesOptions _options;

        public StartupModuleRunner(StartupModulesOptions options)
        {
            _options = options;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            var context = new ConfigureServicesContext(configuration, hostEnvironment, _options);
            foreach (var cfg in _options.StartupModules)
            {
                cfg.ConfigureServices(services, context);
            }
        }

        public void Configure(IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            var context = new ConfigureServicesContext(configuration, hostEnvironment, _options);
            foreach (var cfg in _options.StartupModules)
            {
                cfg.Configure(app, context);
            }
        }
    }
}
