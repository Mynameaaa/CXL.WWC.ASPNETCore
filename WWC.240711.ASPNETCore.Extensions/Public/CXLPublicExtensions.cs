using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace WWC._240711.ASPNETCore.Extensions.Public
{
    public static class CXLPublicExtensions
    {

        /// <summary>
        /// 添加公共配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitPublicConfigure(IServiceCollection services)
        {
            //添加雪花 ID
            var idGeneratorOptions = new IdGeneratorOptions(1) { WorkerIdBitLength = 6 };
            YitIdHelper.SetIdGenerator(idGeneratorOptions);

            return services;
        }

    }
}
