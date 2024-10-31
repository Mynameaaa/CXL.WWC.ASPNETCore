using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

/// <summary>
/// 页面操作项
/// </summary>
public class SystemOperate : SaveUser
{

    /// <summary>
    /// 操作名称
    /// </summary>
    public string OperateName { get; set; }

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

}
