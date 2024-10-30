using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public class SystemMenu
{

    /// <summary>
    /// 菜单 Key
    /// </summary>
    public string MenuKey { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string MenuCode { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

    /// <summary>
    /// 菜单 Url
    /// </summary>
    public string MenuUrl { get; set; }

    /// <summary>
    /// 父级编号
    /// </summary>
    public long? ParentID { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool IsDisable { get; set; }

}
