using System.Globalization;
using Consul;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Roles.Repositories;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users.Repositories;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Repositories;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Seeds;
using Payeh.SharedKernel.Consul;
using Payeh.SharedKernel.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddConsulClient(c =>
{
    c.Address = new Uri("http://127.0.0.1:8500");
});
builder.Services.AddConsulServiceRegistration("identity", "Nezam.EES.Service.Identity","localhost",5001);


// Domain
builder.Services.AddTransient<IUserDomainService, UserDomainService>();
builder.Services.AddTransient<IRoleDomainService, RoleDomainService>();

//EntityFramework 
builder.Services.AddPayehDbContext<IdentityDbContext>(c =>
{
    c.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHostedService<IdentitySeedService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
// Configure request localization for Persian language
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("fa") };
    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddHealthChecks();
// Configure CORS policy to allow any origin, method, and header
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // Allow requests from any origin (necessary for React and other frontends)
            .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader(); // Allow any headers (necessary for API requests)
    });
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
    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }

    dbContext.Database.EnsureCreated();
}



app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseCors(); // Apply CORS middleware globally
app.MapHealthChecks("/health", new HealthCheckOptions { Predicate = r => r.Name.Contains("self") });
await app.RunAsync();