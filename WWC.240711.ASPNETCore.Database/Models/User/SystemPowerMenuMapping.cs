using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public class SystemPowerMenuMapping : CreateUser
{

    /// <summary>
    /// 菜单编号
    /// </summary>
    public long MenuID { get; set; }

    /// <summary>
    /// 权限编号
    /// </summary>
    public long PowerID { get; set; }

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
    /// 权限
    /// </summary>
    public SystemPower Power { get; set; }

    /// <summary>
    /// 菜单
    /// </summary>
    public SystemMenu Menu { get; set; }

}
