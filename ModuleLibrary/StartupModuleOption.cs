using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModuleLibrary
{
    /// <summary>
    /// Specifies options for startup modules.
    /// </summary>
    public class StartupModuleOptions
    {
        /// <summary>
        /// Gets a collection of IStartupModule instances to configure the application with .
        /// </summary>
        public ICollection<IStartupModule> StartupModules { get; } = new List<IStartupModule>();

        /// <summary>
        /// Gets a collection of IApplicationInitializer types to initialize the application with.
        /// </summary>
        public ICollection<Type> ApplicationInitializers { get; } = new List<Type>();

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public IDictionary<string, object> Settings { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Discovers IStartupModule implementations from the entry assembly of the application.
        /// </summary>
        public void DiscoverStartupModules() => DiscoverStartupModules(Assembly.GetEntryAssembly()!);

        public void DiscoverStartupModules(params Assembly[] assemblies)
        {
            //throw new NotImplementedException();
            if (assemblies == null || assemblies.Length == 0 || assemblies.All(a => a == null))
            {
                throw new ArgumentException("No assemblies to discover startup modules from specified.", nameof(assemblies));
            }

            foreach (var type in assemblies.SelectMany(a => a.ExportedTypes))
            {
                if (typeof(IStartupModule).IsAssignableFrom(type))
                {
                    var instance = Activate(type);
                    StartupModules.Add(instance);
                }
                else if (typeof(IApplicationInitializer).IsAssignableFrom(type))
                {
                    ApplicationInitializers.Add(type);
                }
                //else
                //{
                //    throw new ArgumentException(
                //    $"Specified startup module '{type.Name}' does not implement {nameof(IStartupModule)}.",
                //    nameof(type));
                //}
            }
        }

        /// <summary>
        /// Adds the IStartupModule instance of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startupModule"></param>
        public void AddStartupModule<T>(T startupModule) where T : IStartupModule => StartupModules.Add(startupModule);

        public void AddStartupModule<T>() where T : IStartupModule => AddStartupModule(typeof(T));

        private void AddStartupModule(Type type)
        {
            if (typeof(IStartupModule).IsAssignableFrom(type))
            {
                var instance = Activate(type);
                StartupModules.Add(instance);
            }
            else
            {
                throw new ArgumentException(
                  $"Specified startup module '{type.Name}' does not implement {nameof(IStartupModule)}.",
                  nameof(type));
            }
        }

        /// <summary>
        /// Adds aninline middleware configuration to the application.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public void ConfigureMiddleware(Action<IApplicationBuilder, ConfigureMiddlewareContext> action) => StartupModules.Add(new InlineMiddlewareConfiguration(action));

        private IStartupModule Activate(Type type)
        {
            try
            {
                return (IStartupModule)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create instance for {nameof(IStartupModule)} type '{type.Name}'.", ex);
            }
        }
    }
}