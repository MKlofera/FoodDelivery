using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;
using FoodDelivery.Api.DAL.Common.Entities.Auth;
using FoodDelivery.Api.DAL.EF;
using FoodDelivery.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection___LocalDB")!;
builder.Services.AddDbContext<FoodDeliveryDbContext>(opt => opt.UseSqlServer(connectionString: connectionString));



builder.Services.AddIdentity<AppUserEntity, AppRoleEntity>()
    .AddEntityFrameworkStores<FoodDeliveryDbContext>();

builder.Services.AddIdentityServer(options =>
    {
        new SigningAlgorithmOptions(SecurityAlgorithms.RsaSha256) { UseX509Certificate = true };
        options.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);
    })
    .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes)
    .AddInMemoryClients(InMemoryConfiguration.Clients())
    .AddAspNetIdentity<AppUserEntity>();

builder.Services.AddAuthentication()
    .AddOpenIdConnect("oidc", "FoodDelivery IdentityServer", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.SignOutScheme = IdentityServerConstants.SignoutScheme;
        options.SaveTokens = true;

        options.Authority = "https://localhost:7292";
        options.ClientId = "FoodDelivery_Identity";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

// builder.Services.AddAuthentication()
//     .AddOpenIdConnect(Open)

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
