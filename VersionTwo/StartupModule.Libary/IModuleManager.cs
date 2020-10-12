using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModule.Libary
{
    public interface IModuleManager : IDisposable
    {
        void StartModule<TModule>(IServiceCollection services) where TModule : IAppModule;
    }
}
