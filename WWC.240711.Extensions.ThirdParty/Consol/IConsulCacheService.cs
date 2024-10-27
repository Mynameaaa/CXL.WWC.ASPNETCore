using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdPartyCache.CacheKeys;
using WWC._240711.Extensions.ThirdPartyCache.IService;
using WWC._240711.Extensions.ThirdPartyCache.Models;

namespace WWC._240711.Extensions.ThirdParty.Consol
{
    public interface IConsulCacheService : IDisposable
    {

        /// <summary>
        /// 缓存服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceID"></param>
        /// <param name="autoDisposed"></param>
        /// <returns></returns>
        Task<bool> CacheServiceName(string serviceName, string serviceID, bool autoDisposed = false);

        /// <summary>
        /// 停止服务组
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        Task<bool> StopServices(string serviceName);

        /// <summary>
        /// 停止某个服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        Task<bool> StopServiceID(string serviceName, string serviceID);

        /// <summary>
        /// 获取服务名称下任意服务编号
        /// </summary>
        /// <returns></returns>
        Task<string> GetServiceID(string serviceName);

        /// <summary>
        /// 获取服务名称下全部服务
        /// </summary>
        /// <returns></returns>
        Task<HashSet<string>> GetServicesByName(string serviceName);

    }
}
