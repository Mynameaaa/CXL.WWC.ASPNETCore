using WWC._240711.ASPNETCore.Production.Controllers;
using Microsoft.AspNetCore.Mvc;
using WWC._240711.ASPNETCore.Extensions;
using WWC._240711.ASPNETCore.Extensions.Controller.Custom;

var builder = WebApplication.CreateBuilder();

//���ü���
builder.InitConfiguration();

//�Զ��������ļ�
builder.Configuration.AddDeveJsonFile();
builder.Configuration.AddWebConfigFile();
//δʵ��
//builder.Configuration.AddDataBaseConfiguration("");

//����
builder.Services.AddRateLimiterSetup();

//����
builder.Services.AddDefaultCors();

//���ؿ�����
builder.Services.AddCXLControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

//����
app.UseCorsSetup();

app.UseRateLimiterSetup();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/test", ([FromServices] ILogger<WeatherForecastController> logger) =>
{
    EventId eventId = new EventId(666, "MyEventId");
    logger.LogWarning("Very Good");
});

await app.RunAsync();