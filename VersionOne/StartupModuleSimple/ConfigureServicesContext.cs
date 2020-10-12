using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModuleSimple
{
    public class ConfigureServicesContext
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
        public StartupModulesOptions Options { get; }
        public ConfigureServicesContext(IConfiguration configuration, IWebHostEnvironment hostEnvironment, StartupModulesOptions options)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            Options = options;
        }
    }
}
