using Microsoft.AspNetCore.Hosting;
using StartupModuleSimple;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace StartupModules.xUnit
{
    public class WebHostBuilderExtensionsTests
    {
        [Fact]
        public void ThrowsException_WhenNoBuilderSpecified()
        {
            Assert.Throws<ArgumentNullException>("builder", () => StartupModuleSimple.WebHostBuilderExtensions.UseStartupModules(null));
            Assert.Throws<ArgumentNullException>("builder", () => StartupModuleSimple.WebHostBuilderExtensions.UseStartupModules(null, assemblies: Array.Empty<Assembly>()));
            Assert.Throws<ArgumentNullException>("builder", () => StartupModuleSimple.WebHostBuilderExtensions.UseStartupModules(null, configure: _ => { }));
        }

        [Fact]
        public void ThrowsException_WhenNoOptionsConfigurationSpecified()
        {
            var builder = new WebHostBuilder();
            Assert.Throws<ArgumentNullException>("configure", () => builder.UseStartupModules(configure: null));
        }

        [Fact]
        public void ThrowsException_WhenNoAssembliesSpecified()
        {
            var builder = new WebHostBuilder();
            var ex = Assert.Throws<ArgumentException>("assemblies", () => builder.UseStartupModules((Assembly[])null));

            Assert.Equal(new ArgumentException("没有发现任何模块", "assemblies").Message, ex.Message);
        }

        [Fact]
        public void ThrowsException_WhenEmptyssembliesSpecified()
        {
            var builder = new WebHostBuilder();
            var ex = Assert.Throws<ArgumentException>("assemblies", () => builder.UseStartupModules(Array.Empty<Assembly>()));

            Assert.Equal(new ArgumentException("没有发现任何模块", "assemblies").Message, ex.Message);
        }

        [Fact]
        public void ThrowsException_WhenNullAssembliesSpecified()
        {
            var builder = new WebHostBuilder();
            var ex = Assert.Throws<ArgumentException>("assemblies", () => builder.UseStartupModules((Assembly)null));

            Assert.Equal(new ArgumentException("没有发现任何模块", "assemblies").Message, ex.Message);
        }

        [Fact]
        public void Noops_WhenNoStartupModulesAndApplicationInitializers()
        {
            var builder = new WebHostBuilder()
                .Configure(_ => { })
                .UseStartupModules(_ => { })
                .Build();

            Assert.Empty(builder.Services.GetService<IEnumerable<IStartupFilter>>().Where(x => x is ModulesStartupFilter));
        }

        [Fact]
        public void Noops_WhenNoStartupModulesAndApplicationInitializersInEntryAssembly()
        {
            var builder = new WebHostBuilder()
                .Configure(_ => { })
                .UseStartupModules()
                .Build();

            Assert.Empty(builder.Services.GetService<IEnumerable<IStartupFilter>>().Where(x => x is ModulesStartupFilter));
        }
    }
}
