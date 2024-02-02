using CleanArchitecture.Infrastructure;
using CleanArchitecture.Application;
using CleanArchitecture.Services.Poco;
using Microsoft.OpenApi.Models;
using CleanArchitecture.Application.Commons.Exceptions;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));



//for Autherization
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "CLEAN ARCHITECTURE API v1",
        Description = "THIS IS A SAMPLE API ONLY"
    });

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "'Bearer' SPACE and token after \r\n\r\n For Example : \"Bearer eyJhbGci0iJIUzIlNiIsInRScCI6IkpXVCJ9\""
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

});


//For connection String settings
var env = builder.Environment;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Web Api Only");
    });
}
app.ConfigureExceptionHandlingMiddlewere();
app.UseAuthorization();
//app.UseAuthentication();
app.MapControllers();

app.Run();
