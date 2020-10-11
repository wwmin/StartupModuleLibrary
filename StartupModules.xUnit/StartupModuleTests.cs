using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StartupModuleSimple;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StartupModules.xUnit
{
    public class StartupModuleTests
    {
        [Fact]
        public async Task ConfiguresContext()
        {
            var hostBuilder = CreateBuilder().UseStartupModules(o => o.AddStartupModule<FooStartupModule>());
            using var host = hostBuilder.Build();
            await host.StartAsync();
            await host.StopAsync();
        }

        [Fact]
        public async Task DiscoversFromSpecifiedAssembly()
        {
            var hostBuilder = CreateBuilder().UseStartupModules(o => o.DiscoverStartupModules(typeof(FooStartupModule).Assembly));
            using var host = hostBuilder.Build();
            await host.StartAsync();
            await host.StopAsync();
        }

        [Fact]
        public async Task DiscoversFromEntryAssembly()
        {
            // Equivalent of calling UseStartupModules()
            var hostBuilder = CreateBuilder().UseStartupModules(o => o.DiscoverStartupModules());
            using var host = hostBuilder.Build();
            await host.StartAsync();
            await host.StopAsync();
        }

        [Fact]
        public async Task DiscoversFromEntryAssemblyWithDefaultMethod()
        {
            // Equivalent of calling UseStartupModules()
            var hostBuilder = CreateBuilder().UseStartupModules();
            using var host = hostBuilder.Build();
            await host.StartAsync();
            await host.StopAsync();
        }

        private IWebHostBuilder CreateBuilder() => new WebHostBuilder().UseKestrel().Configure(_ => { });

        public class FooStartupModule : IStartupModule
        {
            public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
            {
                Assert.NotNull(services);
                Assert.NotNull(context);
                Assert.NotNull(context.Configuration);
                Assert.NotNull(context.HostEnvironment);
            }

            public void Configure(IApplicationBuilder app, ConfigureServicesContext context)
            {
                Assert.NotNull(app);
                Assert.NotNull(context);
                Assert.NotNull(context.Configuration);
                Assert.NotNull(context.HostEnvironment);
            }
        }
    }
}
