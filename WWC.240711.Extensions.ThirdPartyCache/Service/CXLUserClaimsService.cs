using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Infrastructure;

namespace WWC._240711.Extensions.ThirdPartyCache;

public class CXLUserClaimsService : ICXLUserClaimsService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private AuthUserInfo authUserInfo = null;

    public CXLUserClaimsService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 获取当前用户
    /// </summary>
    /// <returns></returns>
    public AuthUserInfo GetCurrentUser()
    {
        if (authUserInfo == null)
        {
            authUserInfo = _httpContextAccessor.HttpContext.Items["auth-Claims"] as AuthUserInfo;
        }
        return authUserInfo;
    }

    /// <summary>
    /// 获取当前租户编号
    /// </summary>
    /// <returns></returns>
    public int GetTenantID()
    {
        if (authUserInfo == null)
        {
            authUserInfo = _httpContextAccessor.HttpContext.Items["auth-Claims"] as AuthUserInfo;
        }
        return authUserInfo == null ? 0 : authUserInfo.TenantID;
    }

}
