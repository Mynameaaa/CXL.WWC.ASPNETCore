using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Infrastructure
{
    public class AuthUserInfo
    {
        /// <summary>
        /// 多租户编号
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public string UserPower { get; set; }

        /// <summary>
        /// 用户操作范围
        /// </summary>
        public string UserScope { get; set; }

    }
}
