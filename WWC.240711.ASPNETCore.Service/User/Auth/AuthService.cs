using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Database.EFCore;
using WWC._240711.ASPNETCore.Database.Extensions;
using WWC._240711.ASPNETCore.Database.Models;
using WWC._240711.ASPNETCore.Infrastructure;
using WWC._240711.ASPNETCore.Service.User.Dto;
using WWC._240711.Extensions.ThirdPartyCache;

namespace WWC._240711.ASPNETCore.Service.User.Auth
{
    /// <summary>
    /// 权限相关配置
    /// </summary>
    public class AuthService
    {
        private readonly SystemDbContext _dbContext;
        private readonly ICXLUserClaimsService _cXLUserClaimsService;

        public AuthService(SystemDbContext dbContext, ICXLUserClaimsService cXLUserClaimsService)
        {
            _dbContext = dbContext;
            _cXLUserClaimsService = cXLUserClaimsService;
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessageResult> AddRole(CreateRoleDto model)
        {
            var role = new SystemRole()
            {
                RoleKey = model.RoleKey,
                RoleName = model.RoleName
            };
            role.SetSaveUser(_cXLUserClaimsService.GetCurrentUser());

            await _dbContext.SystemRoles.AddAsync(role);
            var success = await _dbContext.SaveChangesAsync() > 0;
            return new MessageResult()
            {
                IsSuccess = success,
                Code = 200,
                Message = "操作成功"
            };
        }

    }
}
