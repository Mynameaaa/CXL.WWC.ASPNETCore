using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Database.Extensions.Enum;
using WWC._240711.ASPNETCore.Database.Models.User;

namespace WWC._240711.ASPNETCore.Database.Models;

/// <summary>
/// 用户资料
/// </summary>
public class UserInfo : SaveUser
{
    /// <summary>
    /// 用户编号
    /// </summary>
    [Key]
    public long UserID { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    [Column(TypeName = "nvarchar(30)")]
    public string UserName { get; set; }

    /// <summary>
    /// 用户手机号
    /// </summary>
    [Column(TypeName = "nvarchar(30)")]
    public string UserPhoneNumber { get; set; }

    /// <summary>
    /// 用户状态
    /// </summary>
    public UserState UserState { get; set; }

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
    /// 用户登录信息
    /// </summary>
    public UserLogin UserLogin { get; set; }

    /// <summary>
    /// 用户角色列表
    /// </summary>
    public ICollection<SystemRole> ListSystemRole { get; set; } = new List<SystemRole>();

    /// <summary>
    /// 用户菜单列表
    /// </summary>
    public ICollection<SystemMenu> ListSystemMenu { get; set; } = new List<SystemMenu>();

}
