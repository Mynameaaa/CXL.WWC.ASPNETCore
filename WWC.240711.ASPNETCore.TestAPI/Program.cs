using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using WWC._240711.ASPNETCore.TestAPI;
using WWC._240711.ASPNETCore.TestAPI.Controllers;
using WWC._240711.ASPNETCore.TestAPI.SwaggerFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "JwtBearer";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateActor = false,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = false,
    };
});

// ����Զ����������·������
builder.Services.AddControllers(options =>
{
    // ����Զ����·�ɹ���
    options.Conventions.Add(new CXLRouteConvention());
    options.Conventions.Add(new ApiExplorerHideOnlyConvention());
})
.ConfigureApplicationPartManager(manager =>
{
    // ����Զ���� CXL �������ṩ����
    manager.FeatureProviders.Add(new CXLControllerFeatureProvider());
});

// ��� Swagger �� ApiExplorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    foreach (var group in typeof(CXLSwaggerGroup).GetFields().Where(f => f.Name != "value__"))
    {
        var docName = group.Name;

        o.SwaggerDoc(docName, new OpenApiInfo()
        {
            Title = docName + "����",
            Version = "v1.0.1",
            Description = docName + "ģ���ĵ�",
            Contact = new OpenApiContact()
            {
                Email = "2244025263@qq.com",
                //Extensions = exntension,
                Name = "CXL---Contact",
                Url = new Uri("http://localhost:5221/CXLdoc/Index.html?urls.primaryName=Stock")
            },
            License = new OpenApiLicense()
            {
                Name = "CXL---License",
                Url = new Uri("http://localhost:5221/CXLdoc/Index.html?urls.primaryName=Order")
            },
            Extensions = new Dictionary<string, IOpenApiExtension>
            {
                { "powered by", new Microsoft.OpenApi.Any.OpenApiString(".net8.0") }
            },
        });
    }

    //���� xml �ļ�
    string basePath = AppContext.BaseDirectory;
    o.IncludeXmlComments(Path.Combine(basePath, "WWC.240711.ASPNETCore.TestAPI.xml"));

    //���������������ÿ���������������һ��
    o.OperationFilter<CXLSecurityOperationFilter>();
    o.OperationFilter<CXLSwaggerGroupOperationFilter>();

    //�ĵ������������ÿ���ĵ������һ��
    o.DocumentFilter<CXLSecurityDocumentFilter>();

    //Schema ���������
    o.SchemaFilter<CXLEnumSchemaFilter>();

    //���Ա���� Obsolete ���Եķ���
    o.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;
    //���Ա���� Obsolete ���Ե�����
    o.SchemaGeneratorOptions.IgnoreObsoleteProperties = true;

    o.AddServer(new OpenApiServer()
    {
        Description = "����ͨ�ŵ�ַ",
        Url = "https://localhost:7226"
    });
    o.AddServer(new OpenApiServer()
    {
        Description = "����ȫ�ĵ�ַ",
        Url = "http://localhost:5221"
    });

}).AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// ���� Swagger
app.UseSwagger(options =>
{
    options.RouteTemplate = "/CXLdoc/{documentName}/swagger.json";
    //options.SerializeAsV2 = true;
});

app.UseSwaggerUI(options =>
{
    // ǿ�ƽ������������
    options.DefaultModelsExpandDepth(-1);  // ��ֹĬ��չ��ģ��
    options.DisplayRequestDuration();  // ��ʾ�������ʱ��
    options.RoutePrefix = "CXLdoc";
    foreach (var group in typeof(CXLSwaggerGroup).GetFields().Where(f => f.Name != "value__"))
    {
        var docName = group.Name;
        options.SwaggerEndpoint($"/CXLdoc/{docName}/swagger.json", docName);
    }
});

app.Use(async (context, next) =>
{
    await next(context);
});

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
