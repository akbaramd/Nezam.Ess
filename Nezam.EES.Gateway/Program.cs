using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Nezam.EES.Gateway;
using Nezam.EES.Service.Identity;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;
using Payeh.SharedKernel.Consul;
using Payeh.SharedKernel.EntityFrameworkCore;
using Payeh.SharedKernel.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(c=>c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddIdentitySlice(builder.Configuration);
//EntityFramework 
builder.Services.AddPayehDbContext<AppDbContext>(c =>
{
    c.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddPayehUnitOfWork(c =>
{
    c.IsUnitOfWorkEnabled = true;
    c.IsTransactional = false;
});

builder.Services.AddAuthenticationJwtBearer(s =>
    {
        s.SigningKey = builder.Configuration["Jwt:SecretKey"]; // Fetch from configuration
    })
    .AddAuthorization()
    .SwaggerDocument()
    .AddFastEndpoints();
// Configure CORS
builder.Services
    .AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy
                .WithOrigins(builder.Configuration.GetSection("CORS:Origins").Get<string[]>() ?? [])
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
    });
// Configure health checks
builder.Services
    .AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);


var app = builder.Build();


app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();
app.UseCors("CorsPolicy");
await app.RunAsync();