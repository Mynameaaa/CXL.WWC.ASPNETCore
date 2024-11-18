using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models.User
{
    public class UserLogin : SaveUser
    {
        /// <summary>
        /// 登录编号
        /// </summary>
        public long LoginID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        [Column(TypeName = "nvarchar(30)")]
        public string UserAccount { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(TypeName = "nvarchar(30)")]
        public string UserPassword { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public long CreateID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人编号
        /// </summary>
        public long UpdateID { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string UpdateName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }

    }
}
