using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModule.Libary
{
    public abstract class AppModule : IAppModule
    {
        public virtual void OnApplicationInitialization()
        {
        }

        public virtual void OnApplicationShutdown()
        {
        }

        public virtual void OnConfigureServices()
        {
        }

        public virtual void OnPostApplicationInitialization()
        {
        }

        public virtual void OnPostConfigureServices()
        {
        }

        public virtual void OnPreApplicationInitialization()
        {
        }

        public virtual void OnPreConfigureServices()
        {
        }
    }
}
