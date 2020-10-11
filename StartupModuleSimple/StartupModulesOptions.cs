using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StartupModuleSimple
{
    public class StartupModulesOptions
    {
        public ICollection<IStartupModule> StartupModules { get; } = new List<IStartupModule>();

        public Dictionary<string, object> Settings = new Dictionary<string, object>();

        public void DiscoverStartupModules() => DiscoverStartupModules(Assembly.GetEntryAssembly()!);

        public void DiscoverStartupModules(params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0 || assemblies.All(a => a == null))
            {
                throw new ArgumentException("没有发现任何模块", nameof(assemblies));
            }
            foreach (var type in assemblies.SelectMany(a => a.ExportedTypes))
            {
                if (typeof(IStartupModule).IsAssignableFrom(type))
                {
                    var instance = Activate(type);
                    StartupModules.Add(instance);
                }
            }
        }


        public void AddStartupModule<T>(T startupModule) where T : IStartupModule => StartupModules.Add(startupModule);

        public void AddStartupModule<T>() where T : IStartupModule => AddStartupModule(typeof(T));

        public void AddStartupModule(Type type)
        {
            if (typeof(IStartupModule).IsAssignableFrom(type))
            {
                var instance = Activate(type);
                StartupModules.Add(instance);
            }
            else
            {
                throw new ArgumentException($"Specified startup module '{type.Name}' does not implement {nameof(IStartupModule)}.", nameof(type));
            }
        }

        private IStartupModule Activate(Type type)
        {
            try
            {
                return (IStartupModule)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"创建实例未 {nameof(IStartupModule)}的实例失败 name: '{type.Name}'.", ex);
                throw;
            }
        }
    }
}
