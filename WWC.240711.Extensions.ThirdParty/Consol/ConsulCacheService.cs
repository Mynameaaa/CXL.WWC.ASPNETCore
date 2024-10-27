using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdPartyCache.CacheKeys;
using WWC._240711.Extensions.ThirdPartyCache.IService;

namespace WWC._240711.Extensions.ThirdParty.Consol
{
    public class ConsulCacheService : IConsulCacheService
    {
        private IStringCacheService _stringCacheService;
        public List<string> _disposedServices = new List<string>();

        public ConsulCacheService(IStringCacheService stringCacheService)
        {
            _stringCacheService = stringCacheService;
        }

        /// <summary>
        /// 缓存 Service
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public async Task<bool> CacheService(string serviceID)
        {
            string key = string.Format(ConsulConstantKey.ConsulServiceKey, serviceID);
            return await _stringCacheService.SetAsync(key, serviceID);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {

        }

    }
}
