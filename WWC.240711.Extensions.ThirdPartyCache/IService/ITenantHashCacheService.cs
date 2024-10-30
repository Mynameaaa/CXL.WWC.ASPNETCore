using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.IService
{
    public interface ITenantHashCacheService : IHashCacheService
    {
        /// <summary>
        /// 获取当前租户 Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetTenantKey(string key);

    }
}
