using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public interface UpdateUser
{
    /// <summary>
    /// 修改人编号
    /// </summary>
    long UpdateID { get; set; }

    /// <summary>
    /// 修改人名称
    /// </summary>
    string UpdateName { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime UpdateTime { get; set; }
}
