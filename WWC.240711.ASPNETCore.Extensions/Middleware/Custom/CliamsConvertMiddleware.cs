using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Infrastructure;

namespace WWC._240711.ASPNETCore.Extensions;

/// <summary>
/// 处理来自请求头的 Claims 信息
/// </summary>
public class CliamsConvertMiddleware
{
    private readonly RequestDelegate _next;

    public CliamsConvertMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var hasClaims = Convert.ToBoolean(context.Request.Headers["auth-HasClaims"]);
        if (hasClaims)
        {
            context.Items["auth-Claims"] = new AuthUserInfo()
            {
                UserID = Convert.ToInt32(context.Request.Headers["userid-C"]),
                TenantID = 1,
                UserPower = context.Request.Headers["userPower-C"].ToString() ?? "",
                UserName = context.Request.Headers["username-C"].ToString() ?? "",
                UserScope = context.Request.Headers["scope-C"].ToString() ?? "",
            };
        }

        await _next(context);
    }


}
