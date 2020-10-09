using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ModuleLibrary
{
    /// <summary>
    /// provide access touseful services during application servies configuration
    /// </summary>
    public class ConfigureServicesContext
    {
        /// <summary>
        /// Provides access to useful services during application services configuration.
        /// </summary>
        public ConfigureServicesContext(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, StartupModuleOptions options)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Options = options;
        }
        /// <summary>
        /// Gets the application IConfiguration instance.
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Gets the application IWebHostEnvironment instance.
        /// </summary>

        public IWebHostEnvironment HostingEnvironment { get; }
        /// <summary>
        /// Gets the StartupModuleOptions.
        /// </summary>

        public StartupModuleOptions Options { get; }
    }
}