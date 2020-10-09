using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModuleLibrary
{
    /// <summary>
    /// Provides extensions to configure the startup modules infrastructure and configure or discover <see cref="IStartupModule"/> instances.
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder) => UseStartupModules(builder, options => options.DiscoverStartupModules());

        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder, params Assembly[] assemblies) => UseStartupModules(builder, options => options.DiscoverStartupModules(assemblies));

        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder, Action<StartupModuleOptions> configure)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (configure == null) throw new ArgumentNullException(nameof(configure));
            var options = new StartupModuleOptions();
            configure(options);
            if (options.StartupModules.Count == 0 && options.ApplicationInitializers.Count == 0)
            {
                // Nothing to do here
                return builder;
            }

            var runner = new StartupModuleRunner(options);
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IStartupFilter>(Span => ActivatorUtilities.CreateInstance<ModulesStartupFilter>(Span, runner));

                var configureServicesContext = new ConfigureServicesContext(hostContext.Configuration, hostContext.HostingEnvironment, options);
                runner.ConfigureServices(services, hostContext.Configuration, hostContext.HostingEnvironment);

            });
            return builder;
        }
    }
}
