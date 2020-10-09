using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModuleLibrary
{
    /// <summary>
    /// Represents a class that initializes application services during startup
    /// </summary>
    public interface IApplicationInitializer
    {
        /// <summary>
        /// Invokes the <see cref="IApplicationInitializer"/> 
        /// </summary>
        /// <returns></returns>
        Task Invoke();
    }
}
