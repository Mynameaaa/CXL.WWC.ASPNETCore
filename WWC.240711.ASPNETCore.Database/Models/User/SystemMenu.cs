using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public class SystemMenu : SaveUser
{
    public long MenuID { get; set; }

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
    /// 权限列表
    /// </summary>
    public ICollection<SystemPower> ListSystemPower { get; set; } = new List<SystemPower>();

    /// <summary>
    /// 操作列表
    /// </summary>
    [InverseProperty(nameof(SystemOperate.Menu))]
    public ICollection<SystemOperate> ListSystemOperate { get; set; } = new List<SystemOperate>();

}
