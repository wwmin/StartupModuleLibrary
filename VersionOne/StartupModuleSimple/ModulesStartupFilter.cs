using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModuleSimple
{
    public class ModulesStartupFilter : IStartupFilter
    {
        private readonly StartupModuleRunner _runner;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ModulesStartupFilter(StartupModuleRunner runner, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _runner = runner;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                _runner.Configure(app, _configuration, _hostEnvironment);
                next(app);
            };
        }
    }
}
