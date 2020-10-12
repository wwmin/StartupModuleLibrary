using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModule.Libary
{
    /// <summary>
    /// 模块依赖的模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// 依赖的模块类型
        /// </summary>
        public Type[] DependModuleTypes { get; private set; }
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="dependModuleTypes"></param>
        public DependsOnAttribute(params Type[] dependModuleTypes)
        {
            DependModuleTypes = dependModuleTypes ?? new Type[0];
        }
    }
}
