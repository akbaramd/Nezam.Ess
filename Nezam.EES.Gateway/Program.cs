using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Payeh.SharedKernel.Consul;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddConsulClient(c=> {c.Address = new Uri("http://127.0.0.1:8500");});
builder.Services.AddConsulServiceRegistration("gateway", "Nezam.EES.Gateway", "localhost", 5000);
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
    .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"])
    ;


// Add Ocelot
var routes = "Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(options => { options.Folder = routes; });
builder.Services.AddOcelot(builder.Configuration)
    .AddConsul();

builder.Services.AddSwaggerForOcelot(builder.Configuration);
// Add Ocelot json file configuration
builder.Configuration.AddJsonFile("ocelot.json");

var app = builder.Build();


app.UseRouting();
app.UseEndpoints(_ => { });


app.MapHealthChecks("/health", new HealthCheckOptions { Predicate = r => r.Name.Contains("self") });

app.UseCors("CorsPolicy");
app.MapGet("/services", async ([FromServices] IConsulService consulService) => await consulService.GetCurrentNodeHealthAsync());
app.UseSwagger();
await app.UseSwaggerForOcelotUI(options => { options.PathToSwaggerGenerator = "/swagger/docs"; }).UseOcelot();

await app.RunAsync();