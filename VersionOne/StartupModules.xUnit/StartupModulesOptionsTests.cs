using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModuleSimple;
using System;
using Xunit;

namespace StartupModules.xUnit
{
    public class StartupModulesOptionsTests
    {
        [Fact]
        public void AddStartupModule_FromGenericType()
        {
            var options = new StartupModulesOptions();
            options.AddStartupModule<FooStartupModule>();

            var module = Assert.Single(options.StartupModules);
            Assert.IsType<FooStartupModule>(module);
        }

        [Fact]
        public void AddStartupModule_FromType()
        {
            var options = new StartupModulesOptions();
            options.AddStartupModule(typeof(FooStartupModule));

            var module = Assert.Single(options.StartupModules);
            Assert.IsType<FooStartupModule>(module);
        }

        [Fact]
        public void AddStartupModule_FromInstance()
        {
            var options = new StartupModulesOptions();
            options.AddStartupModule(new FooStartupModule());

            var module = Assert.Single(options.StartupModules);
            Assert.IsType<FooStartupModule>(module);
        }

        [Fact]
        public void ThrowException_WhenTypeNotIStartupModule()
        {
            var options = new StartupModulesOptions();
            var ex = Assert.Throws<ArgumentException>("type", () => options.AddStartupModule(typeof(decimal)));
            var expectedEx = new ArgumentException($"Specified startup module '{typeof(decimal).Name}' does not implement {nameof(IStartupModule)}.", "type");
            Assert.Equal(expectedEx.Message, ex.Message);
        }

        [Fact]
        public void ThrowsException_WhenNoParameterlessConstructor()
        {
            var options = new StartupModulesOptions();
            var ex = Assert.Throws<InvalidOperationException>(() => options.AddStartupModule<StartupModuleWithCtor>());
            var expectedMsg = $"创建实例未 {nameof(IStartupModule)}的实例失败 name: '{typeof(StartupModuleWithCtor).Name}'.";
            Assert.Equal(expectedMsg, ex.Message);
        }

        [Fact]
        public void ThrowsException_WithErrorConstructor()
        {
            var options = new StartupModulesOptions();
            var ex = Assert.Throws<InvalidOperationException>(() => options.AddStartupModule<StartupModuleWithErrorCtor>());
            var expectedMsg = $"创建实例未 {nameof(IStartupModule)}的实例失败 name: '{typeof(StartupModuleWithErrorCtor).Name}'.";
            //$"Specified startup module '{type.Name}' does not implement {nameof(IStartupModule)}."
            Assert.Equal(expectedMsg, ex.Message);
        }
    }

    internal class StartupModuleWithErrorCtor : IStartupModule
    {
        public StartupModuleWithErrorCtor()
        {
            throw new ArgumentException();
        }

        public void Configure(IApplicationBuilder app, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }
    }

    internal class StartupModuleWithCtor : IStartupModule
    {
        public StartupModuleWithCtor(object _)
        {

        }

        public void Configure(IApplicationBuilder app, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }
    }

    internal class FooStartupModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }
    }
}
