using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using WWC._240711.ASPNETCore.Extensions;
using WWC._240711.ASPNETCore.Extensions.Controller.Custom;
using WWC._240711.ASPNETCore.Extensions.Program.Custom;
using WWC._240711.ASPNETCore.Extensions.Swagger;
using WWC._240711.ASPNETCore.Production.Controllers;

#region Program

//var builder = WebApplication.CreateBuilder();

////配置加载
//builder.InitConfiguration();

////builder.AddCXLServiceContainer();

////自定义配置文件
//builder.Configuration.AddDefaultDeveJsonFile();
//builder.Configuration.AddDefaultWebConfigFile();

////限流
//builder.Services.AddRateLimiterSetup();

////跨域
//builder.Services.AddCXLDefaultCors();

////加载控制器 配置 Json
//builder.Services.AddCXLControllers();

//builder.Services.AddEndpointsApiExplorer();

//#region 未实现

////builder.Services.AddCXLHttpClientOptions();
////builder.Services.ConfigureCXLNamedHttpClient();
////builder.Configuration.AddDataBaseConfiguration("");

//#endregion

////加载 Swagger
//builder.Services.AddCXLSwagger();

//var app = builder.Build();

//app.UseCXLSwagger();

//app.UseHttpsRedirection();

//app.UseRouting();

//app.UseCXLCors();

//app.UseRateLimiterSetup();

//app.UseAuthorization();

//app.MapControllers();

//app.MapGet("/test", ([FromServices] ILogger<WeatherForecastController> logger) =>
//{
//    EventId eventId = new EventId(666, "MyEventId");
//    logger.LogWarning("Very Good");
//});

//await app.RunAsync(); 

#endregion

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args
});

await CXLExtensionsProgram.MainAsync(builder);
