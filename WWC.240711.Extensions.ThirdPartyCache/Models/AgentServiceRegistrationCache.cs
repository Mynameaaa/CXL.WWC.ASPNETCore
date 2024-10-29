using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.Models
{
    public class AgentServiceRegistrationCache
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务唯一标识
        /// </summary>
        public string ServiceID { get; set; }

        /// <summary>
        /// 服务主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务标签
        /// </summary>
        public string[] ServiceTag { get; set; }

        /// <summary>
        /// 所属节点
        /// </summary>
        public string Node { get; set; }

    }
}
