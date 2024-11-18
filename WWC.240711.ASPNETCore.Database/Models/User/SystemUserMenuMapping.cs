﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public class SystemUserMenuMapping : CreateUser
{

    /// <summary>
    /// 用户编号
    /// </summary>
    public long UserID { get; set; }

    /// <summary>
    /// 菜单编号
    /// </summary>
    public long MenuID { get; set; }
    
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
    /// 用户信息
    /// </summary>
    public UserInfo UserInfo { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public SystemMenu Menu { get; set; }

}