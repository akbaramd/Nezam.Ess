using System.Globalization;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Nezam.Modular.ESS.WebApi;

var builder = BonyanApplication.CreateApplicationBuilder<NezamEssModule>(args);

// Add localization services with default culture set to Persian
builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("fa") };
    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Configure FastEndpoints, Swagger, and JWT Authentication
builder.Services.AddFastEndpoints()
    .SwaggerDocument()
    .AddAuthenticationJwtBearer(c =>
    {
        c.SigningKey = "asdsldaosjdisd2364723hy54u23g5835t237854234";
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var app = await builder.BuildAsync();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Use the localization settings configured
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();