using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.Options
{
    public class MemoryCahceOptions
    {

        /// <summary>
        /// 过期检查间隔
        /// </summary>
        public TimeSpan ExpirationScanFrequency { get; set; }

        /// <summary>
        /// 内存最大容量
        /// </summary>
        public int SizeLimit { get; set; }

    }
}
