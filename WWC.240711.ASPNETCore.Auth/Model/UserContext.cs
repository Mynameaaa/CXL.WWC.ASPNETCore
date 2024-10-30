using System.Security.Claims;
using WWC._240711.ASPNETCore.Infrastructure;

namespace WWC._240711.ASPNETCore.Auth;

public class UserContext
{
    public static TokenDataModel TokenDataModel { get; set; } = new TokenDataModel();

    public static string UserName => TokenDataModel.Username;

    public static string Role => TokenDataModel.Role;

    public static void SetContext(List<Claim>? claims)
    {
        if (claims == null)
            return;

        var cliamsKeys = Appsettings.app("");

        TokenDataModel.Role = claims.FirstOrDefault(p => p.Type == "Role")?.Value ?? string.Empty;
        TokenDataModel.Username = claims.FirstOrDefault(p => p.Type == "Name")?.Value ?? string.Empty;
    }

}
