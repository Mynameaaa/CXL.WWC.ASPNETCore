using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Extensions.Enum
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {

        [Description("正常")]
        Normal = 1,

        [Description("禁用")]
        Disable = 5,

        [Description("删除")]
        Delete = 10,


    }
}
