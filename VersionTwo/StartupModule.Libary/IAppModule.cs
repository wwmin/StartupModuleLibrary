using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModule.Libary
{
    /// <summary>
    /// 应用模块接口定义
    /// </summary>
    public interface IAppModule
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        void OnPreConfigureServices();
        /// <summary>
        /// 配置服务
        /// </summary>
        void OnConfigureServices();
        /// <summary>
        /// 配置服务后
        /// </summary>
        void OnPostConfigureServices();
        /// <summary>
        /// 应用启动前
        /// </summary>
        void OnPreApplicationInitialization();
        /// <summary>
        /// 应用启动
        /// </summary>
        void OnApplicationInitialization();
        /// <summary>
        /// 应用启动后
        /// </summary>
        void OnPostApplicationInitialization();
        /// <summary>
        /// 应用停止
        /// </summary>
        void OnApplicationShutdown();
    }
}
