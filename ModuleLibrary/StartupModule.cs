using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ModuleLibrary
{
    /// <summary>
    /// Defines a startup module to configure application service and middleware during startup.
    /// An application candefinemultiplestartup modules froeach of its modules/components/features.
    /// </summary>
    public interface IStartupModule
    {
        /// <summary>
        /// A callback to configure the application's services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="context"></param>
        void ConfigureServices(IServiceCollection services, ConfigureServicesContext context);

        /// <summary>
        /// A callback to configure the middleware pipeline of the application.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="context"></param>
        void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context);
    }
}
