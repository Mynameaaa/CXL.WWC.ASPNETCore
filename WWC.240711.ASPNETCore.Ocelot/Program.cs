using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WWC._240711.ASPNETCore.Auth;
using WWC._240711.ASPNETCore.Auth.Extensions;
using WWC._240711.ASPNETCore.Infrastructure;
using WWC._240711.ASPNETCore.Ocelot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new Appsettings(builder.Configuration));

builder.Services.AddControllers();

//builder.Services.LoadProjectAssemblies();
builder.Services.AddAuthService();

builder.Services.AddCXLOcelot();

builder.Configuration.AddCXLOcelotConfigure();

builder.Services.AddCXLAuthorizationService();

#region JWT ��Ȩ

builder.Services.AddCXLAuthentication();

#endregion

var app = builder.Build();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
    RequestPath = ""
});

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<UserContextMiddleware>();

app.MapControllers();

await app.UseCXLOcelot();

app.Run();
