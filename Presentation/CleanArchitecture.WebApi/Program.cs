using CleanArchitecture.Infrastructure;
using CleanArchitecture.Application;
using CleanArchitecture.Services.Poco;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

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

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
