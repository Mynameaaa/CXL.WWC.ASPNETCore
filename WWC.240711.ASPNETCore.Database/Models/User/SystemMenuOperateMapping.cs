using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models.User
{
    public class SystemMenuOperateMapping : CreateUser
    {

        /// <summary>
        /// 菜单编号
        /// </summary>
        public long MenuID { get; set; }

        /// <summary>
        /// 操作编号
        /// </summary>
        public long OperateID { get; set; }

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

    }
}
