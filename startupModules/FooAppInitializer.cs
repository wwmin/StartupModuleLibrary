using ModuleLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace startupModules
{
    public class FooAppInitializer:IApplicationInitializer
    {
        public Task Invoke()
        {
            return Task.CompletedTask;
        }
    }
}
