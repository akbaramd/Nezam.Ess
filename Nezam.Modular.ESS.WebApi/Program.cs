using System.Globalization;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Application;
using Nezam.Modular.ESS.Infrastructure;
using Nezam.Modular.ESS.Infrastructure.Data;
using Nezam.Modular.ESS.Units.Application;
using Payeh.SharedKernel.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization();
builder.Services.AddIdentityApplication();
builder.Services.AddUnitsApplication();
builder.Services.AddInfrastructure();

// Add DbContext and configure it for SQLite
builder.Services.AddPayehDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configure request localization for Persian language
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("fa") };
    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Configure FastEndpoints, Swagger, and JWT Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthenticationJwtBearer(s =>
    {
        s.SigningKey = builder.Configuration["Jwt:SecretKey"]; // Fetch from configuration
    })
    .AddAuthorization()
    .SwaggerDocument()
    .AddFastEndpoints();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Ensure the database is created at startup (on publish)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();

await app.RunAsync();