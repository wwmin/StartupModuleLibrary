using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StartupModuleSimple
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder) => UseStartupModules(builder, options => options.DiscoverStartupModules());

        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder, params Assembly[] assemblies) => UseStartupModules(builder, options => options.DiscoverStartupModules(assemblies));

        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder, Action<StartupModulesOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            var options = new StartupModulesOptions();
            options.DiscoverStartupModules();
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            configure(options);
            if (options.StartupModules.Count == 0)
            {
                return builder;
            }

            var runner = new StartupModuleRunner(options);
            builder.ConfigureServices((hostContext, services) =>
            {
                //注册 IStartupFilter实现
                services.AddSingleton<IStartupFilter>(sp => ActivatorUtilities.CreateInstance<ModulesStartupFilter>(sp, runner));
                runner.ConfigureServices(services, hostContext.Configuration, hostContext.HostingEnvironment);
            });
            return builder;
        }
    }
}
