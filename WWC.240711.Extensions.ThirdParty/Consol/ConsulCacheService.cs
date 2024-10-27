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
    public class ConsulCacheService : IConsulCacheService
    {
        private readonly IStringCacheService _stringCacheService;
        private readonly ISetCacheService _setCacheService;
        //public List<(CacheType, string)> _disposedServiceKey = new List<(CacheType, string)>();

        public Dictionary<string, CacheType> _disposedServiceKey = new Dictionary<string, CacheType>();

        public ConsulCacheService(IStringCacheService stringCacheService
            , ISetCacheService setCacheService)
        {
            _stringCacheService = stringCacheService;
            _setCacheService = setCacheService;
        }

        ///// <summary>
        ///// 缓存 Tag
        ///// </summary>
        ///// <param name="tagName"></param>
        ///// <returns></returns>
        //public async Task<bool> CacheServiceTag(string tagName)
        //{
        //    string key = string.Format(ConsulConstantKey.ConsulTagKey, tagName);
        //    return await _stringCacheService.SetAsync(key, tagName);
        //}

        /// <summary>
        /// 缓存服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceID"></param>
        /// <param name="autoDisposed"></param>
        /// <returns></returns>
        public async Task<bool> CacheServiceName(string serviceName, string serviceID, bool autoDisposed = false)
        {
            string key = string.Format(ConsulConstantKey.ConsulNameKey, serviceName);
            var result = await _setCacheService.AddToSetAsync(key, serviceID);
            if (result && !_disposedServiceKey.Any(p => p.Value.Equals(key)) && autoDisposed)
            {
                _disposedServiceKey.Add(key, CacheType.Set);
            }
            return result;
        }

        /// <summary>
        /// 停止服务组
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<bool> StopServices(string serviceName)
        {
            string key = string.Format(ConsulConstantKey.ConsulNameKey, serviceName);
            return await _setCacheService.ClearSetAsync(key);
        }

        /// <summary>
        /// 停止某个服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public async Task<bool> StopServiceID(string serviceName, string serviceID)
        {
            string key = string.Format(ConsulConstantKey.ConsulNameKey, serviceName);
            return await _setCacheService.RemoveFromSetAsync(key, serviceID);
        }

        /// <summary>
        /// 获取服务名称下任意服务编号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetServiceID(string serviceName)
        {
            string key = string.Format(ConsulConstantKey.ConsulNameKey, serviceName);
            return await _setCacheService.GetRandomMemberAsync(key);
        }

        /// <summary>
        /// 获取服务名称下全部服务
        /// </summary>
        /// <returns></returns>
        public async Task<HashSet<string>> GetServicesByName(string serviceName)
        {
            string key = string.Format(ConsulConstantKey.ConsulNameKey, serviceName);
            return await _setCacheService.GetAllMembersAsync(key);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            foreach (var item in _disposedServiceKey)
            {
                switch (item.Value)
                {
                    case CacheType.String:
                        break;
                    case CacheType.Set:
                        _setCacheService.ClearSet(item.Key);
                        break;
                    case CacheType.ZSet:
                        break;
                    case CacheType.Hash:
                        break;
                    case CacheType.List:
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
