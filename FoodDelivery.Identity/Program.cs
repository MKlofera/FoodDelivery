using FoodDelivery.Api.DAL.Common.Entities.Auth;
using FoodDelivery.Api.DAL.EF;
using FoodDelivery.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection___LocalDB")!;
builder.Services.AddDbContext<FoodDeliveryDbContext>(opt => opt.UseSqlServer(connectionString: connectionString));



builder.Services.AddIdentity<AppUserEntity, AppRoleEntity>()
    .AddEntityFrameworkStores<FoodDeliveryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes)
    .AddInMemoryClients(InMemoryConfiguration.Clients())
    .AddAspNetIdentity<AppUserEntity>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
