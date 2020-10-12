using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModule.Libary
{
    public class ModuleManager : IModuleManager
    {
        public static string _moduleInterfaceTypeFullName = typeof(IAppModule).FullName;
        public void StartModule<TModule>(IServiceCollection services) where TModule : IAppModule
        {
            throw new NotImplementedException();
        }

        protected virtual List<ModuleDescriptor> VisitModule(Type moduleType)
        {
            var moduleDescriptors = new List<ModuleDescriptor>();
            if (moduleType.IsAbstract || moduleType.IsInterface || moduleType.IsGenericType || !moduleType.IsClass)
            {
                return moduleDescriptors;
            }

            var baseInterfaceType = moduleType.GetInterface(_moduleInterfaceTypeFullName, false);
            if (baseInterfaceType == null)
            {
                return moduleDescriptors;
            }
            var dependModulesAttribute = moduleType.GetCustomAttribute<DependsOnAttribute>();
            if (dependModulesAttribute == null)
            {
                moduleDescriptors.Add(new ModuleDescriptor(moduleType));
            }
            else
            {
                var dependModuleDescriptors = new List<ModuleDescriptor>();
                foreach (var dependModuleType in dependModulesAttribute.DependModuleTypes)
                {
                    dependModuleDescriptors.AddRange(VisitModule(dependModuleType));
                }
                moduleDescriptors.Add(new ModuleDescriptor(moduleType, dependModuleDescriptors.ToArray()));
            }

            return moduleDescriptors;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
