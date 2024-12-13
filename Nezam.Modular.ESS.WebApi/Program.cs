using System.Globalization;
using FastEndpoints;
using FastEndpoints.Swagger;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Localization;
using Nezam.Modular.ESS.Identity.Application;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Infrastructure;
using Nezam.Modular.ESS.Units.Application;
using Nezam.Modular.ESS.WebApi.Endpoints.Users;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);
// Add localization services with default culture set to Persian
builder.Services.AddLocalization();
builder.Services.AddIdentityApplication();
builder.Services.AddUnitsApplication();
builder.Services.AddInfrastructure();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("fa") };
    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Configure FastEndpoints, Swagger, and JWT Authentication
builder.Services.AddFastEndpoints()
    .SwaggerDocument();
builder.Services.AddGraphQLServer()
    .AddQueryType<UserQueries>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .ModifyOptions(x =>
    {
        x.RemoveUnreachableTypes = true;
        x.StrictValidation = false;
    })
    .ModifyRequestOptions(options =>
    {
        options.IncludeExceptionDetails = true; // Debugging option
    })
    .SetPagingOptions(new PagingOptions
    {
        DefaultPageSize = 10,
        MaxPageSize = 50
    });


builder.Services.AddEndpointsApiExplorer();

var app= builder.Build();
app
.UseFastEndpoints()
.UseSwaggerGen();

app.MapGraphQL("/graphql");
await app.RunAsync();


