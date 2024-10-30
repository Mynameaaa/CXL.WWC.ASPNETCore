using CSRedis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdPartyCache.IService;

namespace WWC._240711.Extensions.ThirdPartyCache.Service
{
    public class TenantRedisStringCacheService : ITenantStringCacheService
    {
        private CSRedisClient _cache;
        private ICXLUserClaimsService _cXLUserClaimsService;

        public TenantRedisStringCacheService(CSRedisClient cache, ICXLUserClaimsService cXLUserClaimsService)
        {
            _cache = cache;
            _cXLUserClaimsService = cXLUserClaimsService;
        }

        /// <summary>
        /// 获取当前租户 Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTenantKey(string key)
        {
            return _cXLUserClaimsService.GetTenantID() + "-" + key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> SetAsync(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                throw new Exception("SetAsync 提供了为空的 Key 或 Value");
            key = this.GetTenantKey(key);
            return await _cache.SetAsync(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string value)
        {
            return this.SetAsync(key, value).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> SetSerializeAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || value is null)
                throw new Exception("SetSerializeAsync 提供了为空的 Key 或 Value");
            var jsonObject = JsonConvert.SerializeObject(value);
            return await this.SetAsync(key, jsonObject);
        }

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("GetAsync 提供了为空的 Key");
            key = this.GetTenantKey(key);
            return await _cache.GetAsync(key);
        }

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("GetAsync 提供了为空的 Key");
            return this.GetAsync(key).Result;
        }

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetSerializeAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("GetAsync 提供了为空的 Key");
            key = this.GetTenantKey(key);
            var result = await _cache.GetAsync(key);
            if (result == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
