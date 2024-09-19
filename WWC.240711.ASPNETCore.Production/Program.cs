using WWC._240711.ASPNETCore.Production.Controllers;
using Microsoft.AspNetCore.Mvc;
using WWC._240711.ASPNETCore.Extensions;
using WWC._240711.ASPNETCore.Extensions.Controller.Custom;
using WWC._240711.ASPNETCore.Extensions.Swagger;
using WWC._240711.ASPNETCore.Extensions.Options.Custom;

var builder = WebApplication.CreateBuilder();

//���ü���
builder.InitConfiguration();

//�Զ��������ļ�
builder.Configuration.AddDefaultDeveJsonFile();
builder.Configuration.AddDefaultWebConfigFile();
//δʵ��
//builder.Configuration.AddDataBaseConfiguration("");

//����
builder.Services.AddRateLimiterSetup();

//����
builder.Services.AddCXLDefaultCors();

//���ؿ�����
builder.Services.AddCXLControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCXLHttpClientOptions();

builder.Services.ConfigureCXLNamedHttpClient();

builder.Services.AddCXLSwagger();

var app = builder.Build();

app.UseCXLSwagger();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCXLCors();

app.UseRateLimiterSetup();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/test", ([FromServices] ILogger<WeatherForecastController> logger) =>
{
    EventId eventId = new EventId(666, "MyEventId");
    logger.LogWarning("Very Good");
});

await app.RunAsync();