using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModuleSimple;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StartupModules.xUnit
{
    public class StartupRunnerTests
    {
        [Fact]
        public void ConfiguresServices()
        {
            // Arrange
            var options = new StartupModulesOptions();
            options.AddStartupModule<MyStartupModule>();
            var runner = new StartupModuleRunner(options);
            var services = new ServiceCollection();

            // Act
            runner.ConfigureServices(services, null, null);

            // Assert
            var sd = Assert.Single(services);
            Assert.Equal(typeof(MyStartupModule.MyService), sd.ImplementationType);
        }

        [Fact]
        public void Configures()
        {
            // Arrange
            var options = new StartupModulesOptions();
            var startupModule = new MyStartupModule();
            options.AddStartupModule(startupModule);
            var runner = new StartupModuleRunner(options);

            // Act
            runner.Configure(new ApplicationBuilder(new ServiceCollection().BuildServiceProvider()), null, null);

            // Assert
            Assert.True(startupModule.Configured);
        }
    }

    public class MyStartupModule : IStartupModule
    {
        public class MyService { }

        public bool Configured { get; private set; }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context) => services.AddSingleton<MyService>();

        public void Configure(IApplicationBuilder app, ConfigureServicesContext context) => Configured = true;
    }

}
