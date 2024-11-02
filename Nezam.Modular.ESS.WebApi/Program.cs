using System.Text;
using Bonyan.User;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nezam.Modular.ESS.Identity.Application.Auth;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;
using Nezam.Modular.ESS.WebApi;

var builder = BonyanApplication.CreateApplicationBuilder<NezamEssModule>(args);

builder.Services.AddFastEndpoints().SwaggerDocument().AddAuthenticationJwtBearer(c =>
{
    c.SigningKey = "asdsldaosjdisd2364723hy54u23g5835t237854234";
});

builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = await builder.BuildAsync();

app.UseHttpsRedirection();
app//add this
    .UseFastEndpoints()
    .UseSwaggerGen();;




app.Run();