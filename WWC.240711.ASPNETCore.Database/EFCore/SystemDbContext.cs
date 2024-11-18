using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Database.Models;
using WWC._240711.ASPNETCore.Database.Models.User;

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
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {

        #region 权限相关关系配置

        //菜单与操作
        builder.Entity<SystemOperate>()
            .HasOne(e => e.Menu)
            .WithMany(e => e.ListSystemOperate)
            .HasForeignKey(e => e.MenuID)
            .HasPrincipalKey(e => e.MenuID)
            .IsRequired();

        //菜单与权限
        builder.Entity<SystemMenu>()
            .HasMany(e => e.ListSystemPower)
            .WithMany(e => e.ListSystemMenu)
            .UsingEntity<SystemPowerMenuMapping>(
                l => l.HasOne(e => e.Power).WithMany().HasForeignKey(e => e.PowerID).HasPrincipalKey(e => e.PowerID),
                r => r.HasOne(e => e.Menu).WithMany().HasForeignKey(e => e.MenuID).HasPrincipalKey(e => e.MenuID));

        //权限与角色
        builder.Entity<SystemRole>()
            .HasMany(e => e.ListSystemPower)
            .WithMany(e => e.ListSystemRole)
            .UsingEntity<SystemRolePowerMapping>(
                l => l.HasOne(e => e.Power).WithMany().HasForeignKey(e => e.PowerID).HasPrincipalKey(e => e.PowerID),
                r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleID).HasPrincipalKey(e => e.RoleID));

        //用户与角色
        builder.Entity<UserInfo>()
            .HasMany(e => e.ListSystemRole)
            .WithMany(e => e.ListUserInfo)
            .UsingEntity<SystemUserRoleMapping>(
                l => l.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleID).HasPrincipalKey(e => e.RoleID),
                r => r.HasOne(e => e.UserInfo).WithMany().HasForeignKey(e => e.UserID).HasPrincipalKey(e => e.UserID));

        //用户与菜单
        builder.Entity<UserInfo>()
            .HasMany(e => e.ListSystemMenu)
            .WithMany()
            .UsingEntity<SystemUserMenuMapping>(
                l => l.HasOne(e => e.Menu).WithMany().HasForeignKey(e => e.MenuID).HasPrincipalKey(e => e.MenuID),
                r => r.HasOne(e => e.UserInfo).WithMany().HasForeignKey(e => e.UserID).HasPrincipalKey(e => e.UserID));

        //用户与登录信息
        builder.Entity<UserInfo>()
            .HasOne(e => e.UserLogin)
            .WithOne(e => e.UserInfo)
            .HasForeignKey<UserLogin>(e => e.UserID)
            .HasPrincipalKey<UserInfo>(e => e.UserID);

        //复合主键配置
        builder.Entity<SystemPowerMenuMapping>()
            .HasKey(e => new { e.MenuID, e.PowerID });

        builder.Entity<SystemRolePowerMapping>()
            .HasKey(e => new { e.PowerID, e.Role });

        builder.Entity<SystemUserRoleMapping>()
            .HasKey(e => new { e.Role, e.UserID });

        builder.Entity<SystemUserMenuMapping>()
            .HasKey(e => new { e.MenuID, e.UserID });

        #endregion

        base.OnModelCreating(builder);
    }

}
