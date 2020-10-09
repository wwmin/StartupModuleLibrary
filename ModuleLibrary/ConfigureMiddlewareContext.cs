using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace ModuleLibrary
{
    /// <summary>
    /// Provides access to usefull services during middleware configuration.
    /// </summary>
    public class ConfigureMiddlewareContext
    {
        public ConfigureMiddlewareContext(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IServiceProvider serviceProvider, StartupModuleOptions options)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Options = options;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public IServiceProvider ServiceProvider { get; }

        public StartupModuleOptions Options { get; set; }
    }
}