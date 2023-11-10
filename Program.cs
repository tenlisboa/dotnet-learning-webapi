using LearnApi;
using LearnApi.Infrastructure.Repository;
using LearnApi.Infrastructure;
using LearnApi.Domain.Models;
using LearnApi.Application.Services;
using Microsoft.EntityFrameworkCore;
using LearnApi.Application.Mapping;
using LearnApi.Application.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using LearnApi.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));

ApiVersioning.Configure(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

SwaggerConfigGenerator.Configure(builder);

builder.Services.AddOptions();
builder.Services.Configure<Config>(builder.Configuration.GetSection("Config"));

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

Authentication.Configure(builder);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowEveryone",
    policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
var versionDescriptorProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Build a swagger endpoint for each discovered API version
        foreach (var description in versionDescriptorProvider.ApiVersionDescriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors("AllowEveryone");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
