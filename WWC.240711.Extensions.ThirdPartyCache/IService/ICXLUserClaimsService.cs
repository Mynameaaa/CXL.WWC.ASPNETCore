using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Infrastructure;

namespace WWC._240711.Extensions.ThirdPartyCache;

public interface ICXLUserClaimsService
{

    /// <summary>
    /// 获取当前用户
    /// </summary>
    /// <returns></returns>
    AuthUserInfo GetCurrentUser();

    /// <summary>
    /// 获取当前租户编号
    /// </summary>
    /// <returns></returns>
    int GetTenantID();

}
