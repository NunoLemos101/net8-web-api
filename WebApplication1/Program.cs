using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using WebApplication1.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<KeyVaultReader>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("ForecastReader", policyBuilder =>
    {
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" });
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
