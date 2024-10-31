using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Database.Models;

namespace WWC._240711.ASPNETCore.Database.EFCore;

/// <summary>
/// 系统相关数据表
/// </summary>
public class SystemDbContext : DbContext
{

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemMenu> SystemMenus { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemPower> SystemPowers { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemPowerMenuMapping> SystemPowerMenuMappings { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemRole> SystemRoles { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemRolePowerMapping> SystemRolePowerMappings { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemUserMenuMapping> SystemUserMenuMappings { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<SystemUserRoleMapping> SystemUserRoleMappings { get; set; }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public virtual DbSet<UserInfo> UserInfo { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
