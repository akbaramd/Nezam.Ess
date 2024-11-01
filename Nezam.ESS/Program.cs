using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Nezam.ESS.backend;
using Nezam.ESS.backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "Neza23423423m423324ES233SS234yst3424e33242234mI42d") //add 3423s
    .AddAuthorization()
    .AddFastEndpoints()
    .AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    })
    .SwaggerDocument();


builder.Services.AddDbContext<AppDbContext>(c =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    c.UseSqlServer(connectionString);
    c.EnableSensitiveDataLogging();
    c.ConfigureWarnings(c => c.Throw());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");
app
    .UseCors("AllowAll")
    .UseAuthentication() //add this
    .UseAuthorization()
    .UseDefaultExceptionHandler()
    .UseFastEndpoints()
    .UseSwaggerGen();
app.Run();

namespace Nezam.ESS.backend
{
    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}