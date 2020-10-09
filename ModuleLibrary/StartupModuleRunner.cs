using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleLibrary
{
    internal class StartupModuleRunner
    {
        private StartupModuleOptions _options;

        public StartupModuleRunner(StartupModuleOptions options)
        {
            this._options = options;
        }

        public void ConfigureServices(IServiceCollection services,IConfiguration configuration,IWebHostEnvironment hostingEnvironment)
        {
            var ctx = new ConfigureServicesContext(configuration, hostingEnvironment, _options);
            foreach (var cfg in _options.StartupModules)
            {
                cfg.ConfigureServices(services, ctx);
            }
        }

        public void Configure(IApplicationBuilder app,IConfiguration configuration,IWebHostEnvironment hostingEnvironment)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var ctx = new ConfigureMiddlewareContext(configuration, hostingEnvironment, scope.ServiceProvider, _options);
            foreach (var cfg in _options.StartupModules)
            {
                cfg.Configure(app, ctx);
            }
        }


        /// <summary>
        /// Invokes the discovered <see cref="IApplicationInitializer"/> instances.
        /// </summary>
        /// <param name="serviceProvider">The application's root service provider.</param>
        public async Task RunApplicationInitializers(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var applicationInitializers = _options.ApplicationInitializers
                .Select(t =>
                {
                    try
                    {
                        return ActivatorUtilities.CreateInstance(scope.ServiceProvider, t);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Failed to create instace of {nameof(IApplicationInitializer)} '{t.Name}'.", ex);
                    }
                })
                .Cast<IApplicationInitializer>();

            foreach (var initializer in applicationInitializers)
            {
                try
                {
                    await initializer.Invoke();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"An exception occured during the execution of {nameof(IApplicationInitializer)} '{initializer.GetType().Name}'.", ex);
                }
            }
        }
    }
}