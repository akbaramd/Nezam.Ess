using Starterkit._keenthemes;
using Starterkit._keenthemes.libs;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// Configure Authentication and Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/signin";
    options.AccessDeniedPath = "/system/error";
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IKTTheme, KTTheme>();
builder.Services.AddSingleton<IKTBootstrapBase, KTBootstrapBase>();

IConfiguration themeConfiguration = new ConfigurationBuilder()
                            .AddJsonFile("_keenthemes/config/themesettings.json")
                            .Build();

IConfiguration iconsConfiguration = new ConfigurationBuilder()
                            .AddJsonFile("_keenthemes/config/icons.json")
                            .Build();

KTThemeSettings.init(themeConfiguration);
KTIconsSettings.init(iconsConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseThemeMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboards}/{action=Index}/{id?}");

app.Run();
