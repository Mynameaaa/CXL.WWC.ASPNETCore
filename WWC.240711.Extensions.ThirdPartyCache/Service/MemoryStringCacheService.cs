using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdPartyCache.IService;

namespace WWC._240711.Extensions.ThirdPartyCache.Service
{
    public class MemoryStringCacheService : IStringCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryStringCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("Get 提供了为空的 Key");

            string result;
            if (_memoryCache.TryGetValue(key, out result))
            {
                return result;
            }
            return null;
        }

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSerializeAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetSerializeAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}
