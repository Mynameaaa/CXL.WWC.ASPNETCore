using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public interface CreateUser
{

    /// <summary>
    /// 创建人编号
    /// </summary>
    long CreateID { get; set; }

    /// <summary>
    /// 创建人名称
    /// </summary>
    string CreateName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreateTime { get; set; }

}
