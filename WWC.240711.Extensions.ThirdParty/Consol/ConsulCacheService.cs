using Consul;
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
        private readonly IHashCacheService _hashCacheService;

        public ConsulCacheService(IStringCacheService stringCacheService
            , ISetCacheService setCacheService
            , IHashCacheService hashCacheService)
        {
            _stringCacheService = stringCacheService;
            _setCacheService = setCacheService;
            _hashCacheService = hashCacheService;
        }

        public Dictionary<string, CacheType> _disposedServiceKey = new Dictionary<string, CacheType>();

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
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            var result = await _setCacheService.AddToSetAsync(key, serviceID);
            if (result && !_disposedServiceKey.Any(p => p.Value.Equals(key)) && autoDisposed)
            {
                _disposedServiceKey.Add(key, CacheType.Set);
            }
            return result;
        }

        /// <summary>
        /// 缓存服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceID"></param>
        /// <param name="autoDisposed"></param>
        /// <returns></returns>
        public async Task<bool> CacheService(string serviceName, AgentServiceRegistration registration, bool autoDisposed = false)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            var cacheModel = new AgentServiceRegistrationCache()
            {
                Host = registration.Address,
                Port = registration.Port,
                ServiceID = registration.ID,
                ServiceName = registration.Name,
                ServiceTag = registration.Tags
            };
            var result = await _hashCacheService.AddToHashAsync(key, cacheModel.ServiceID, cacheModel);
            if (result && !_disposedServiceKey.Any(p => p.Value.Equals(key)) && autoDisposed)
            {
                _disposedServiceKey.Add(key, CacheType.Hash);
            }
            return result;
        }

        ///// <summary>
        ///// 停止服务组
        ///// </summary>
        ///// <param name="serviceName"></param>
        ///// <returns></returns>
        //public async Task<bool> StopServices(string serviceName)
        //{
        //    string key = string.Format(ConsulConstantKey.ConsulNameKey, serviceName);
        //    return await _setCacheService.ClearSetAsync(key);
        //}

        /// <summary>
        /// 停止服务组
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<bool> StopServices(string serviceName)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            var result = _disposedServiceKey.Remove(key);
            if (result)
                return await _hashCacheService.ClearHashAsync(key);
            else
                return result;
        }

        /// <summary>
        /// 停止某个服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public async Task<bool> StopServiceID(string serviceName, string serviceID)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            return await _hashCacheService.RemoveFieldAsync(key, serviceID);
        }

        /// <summary>
        /// 获取服务名称下任意服务
        /// </summary>
        /// <returns></returns>
        public async Task<AgentServiceRegistrationCache> GetRandomService(string serviceName)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            return await _hashCacheService.GetRandomFieldValueAsync<AgentServiceRegistrationCache>(key);
        }

        /// <summary>
        /// 获取服务名称下全部服务
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AgentServiceRegistrationCache>> GetAllServicesByName(string serviceName)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            return await _hashCacheService.GetAllFieldsAsync<AgentServiceRegistrationCache>(key);
        }

        /// <summary>
        /// 缓存一组服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="registration"></param>
        /// <param name="autoDisposed"></param>
        /// <returns></returns>
        public async Task<bool> CacheServices(string serviceName, IEnumerable<AgentServiceRegistrationCache> registration, bool autoDisposed = false)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceNameKey, serviceName);
            var cacheDirs = registration.Select(sl =>
            {
                return new KeyValuePair<string, AgentServiceRegistrationCache>(sl.ServiceID, sl);
            }).ToDictionary();
            return await _hashCacheService.AddMultipleToHashAsync(key, cacheDirs);
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
