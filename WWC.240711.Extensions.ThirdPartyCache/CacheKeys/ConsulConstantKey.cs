using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.CacheKeys
{
    /// <summary>
    /// Consul 缓存相关 Key
    /// </summary>
    public class ConsulConstantKey
    {

        //Consul服务标签
        public const string ConsulServiceTagKey = "ConsulTag:{0}";

        //Consul服务名称
        public const string ConsulServiceNameKey = "ConsulName:{0}";

    }
}
