using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace ModuleLibrary
{
    internal class ModulesStartupFilter : IStartupFilter
    {
        private readonly StartupModuleRunner _runner;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ModulesStartupFilter(StartupModuleRunner runner, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _runner = runner;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
          {
              _runner.Configure(app, _configuration, _hostingEnvironment);
              _runner.RunApplicationInitializers(app.ApplicationServices).GetAwaiter().GetResult();
              next(app);
          };
    }
}