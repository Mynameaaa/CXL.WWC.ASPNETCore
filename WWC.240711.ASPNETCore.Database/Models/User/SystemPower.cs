using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public class SystemPower : SaveUser
{
    public long PowerID { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PowerName { get; set; }


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
    /// 菜单列表
    /// </summary>
    public ICollection<SystemMenu> ListSystemMenu { get; set; } = new List<SystemMenu>();

    /// <summary>
    /// 权限列表
    /// </summary>
    public ICollection<SystemRole> ListSystemRole { get; set; } = new List<SystemRole>();

}
