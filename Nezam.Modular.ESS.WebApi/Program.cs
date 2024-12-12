using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Nezam.Modular.ESS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add localization services with default culture set to Persian
builder.Services.AddLocalization();

builder.Services.AddInfrastructure();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("fa") };
    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Configure FastEndpoints, Swagger, and JWT Authentication
// builder.Services.AddFastEndpoints()
    // .SwaggerDocument();


builder.Services.AddEndpointsApiExplorer();

var app= builder.Build();
// app
// .UseFastEndpoints()
// .UseSwaggerGen();
await app.RunAsync();


