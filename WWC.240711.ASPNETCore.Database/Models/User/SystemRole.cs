using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public class SystemRole : SaveUser
{
    [Key]
    public long RoleID { get; set; }

    /// <summary>
    /// 角色 Key
    /// </summary>
    public string RoleKey { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

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
    /// 用户列表
    /// </summary>
    public ICollection<UserInfo> ListUserInfo { get; set; } = new List<UserInfo>();

}
