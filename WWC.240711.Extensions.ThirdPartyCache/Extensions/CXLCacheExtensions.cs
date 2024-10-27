using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocelot.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Infrastructure;
using WWC._240711.Extensions.ThirdParty.Models;
using WWC._240711.Extensions.ThirdPartyCache.IService;
using WWC._240711.Extensions.ThirdPartyCache.Options;
using WWC._240711.Extensions.ThirdPartyCache.Service;

namespace WWC._240711.Extensions.ThirdPartyCache.Redis
{
    public static class CXLCacheExtensions
    {

        /// <summary>
        /// 添加 Redis 作为缓存
        /// </summary>`
        /// <returns></returns>
        public static IServiceCollection AddCXLRedisCache(this IServiceCollection services, List<RedisConnectOptions> redisConnects = null)
        {
            if (redisConnects == null || !redisConnects.Any())
            {
                redisConnects = Appsettings.app<List<RedisConnectOptions>?>("RedisConnectOptions");
                if (redisConnects == null || !redisConnects.Any())
                    return services;
            }

            string[] redisClusterNodes = AssembleConnect(redisConnects);
            var csredis = new CSRedisClient(null, redisClusterNodes);
            RedisHelper.Initialization(csredis);
            services.TryAddTransient<IStringCacheService, RedisStringCacheService>();
            services.TryAddTransient<ITenantStringCacheService, TenantRedisStringCacheService>();
            return services.AddSingleton<CSRedisClient>(csredis);
        }

        /// <summary>
        /// 添加内存缓存
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCXLMemoryCache(this IServiceCollection services, MemoryCahceOptions cahceOptions = null)
        {
            if (cahceOptions is null)
                cahceOptions = Appsettings.app<MemoryCahceOptions?>("MemoryCahceOptions");

            services.TryAddTransient<IStringCacheService, MemoryStringCacheService>();
            return services.AddMemoryCache();
        }

        /// <summary>
        /// 组装链接信息
        /// </summary>
        /// <param name="connectOptions"></param>
        /// <returns></returns>
        private static string[] AssembleConnect(List<RedisConnectOptions> connectOptions)
        {

            List<string> results = new List<string>();

            foreach (var connect in connectOptions)
            {
                StringBuilder connectSB = new StringBuilder();
                connectSB.Append(connect.Host);
                connectSB.Append(":");
                connectSB.Append(connect.Port);

                if (!string.IsNullOrEmpty(connect.Password))
                    connectSB.Append($";password={connect.Password}");

                if (connect.ConnectTimeout.HasValue)
                    connectSB.Append($";connectTimeout={connect.ConnectTimeout}");

                if (connect.ReadTimeout.HasValue)
                    connectSB.Append($";readtimeout={connect.ReadTimeout}");

                if (connect.WriteTimeout.HasValue)
                    connectSB.Append($";writetimeout={connect.WriteTimeout}");

                if (connect.PoolSize.HasValue)
                    connectSB.Append($";poolSize={connect.PoolSize}");

                if (connect.MaxIdleTime.HasValue)
                    connectSB.Append($";maxIdleTime={connect.MaxIdleTime}");

                results.Add(connectSB.ToString());
            }
            return results.ToArray();
        }
    }
}
