using System.Diagnostics;
using System.Text;
using AutoMapper;
using AutoMapper.Internal;

using FoodDelivery.Api.BL.Facades;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.BL.Installer;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.EF;
using FoodDelivery.Api.DAL.EF.Extensions;
using FoodDelivery.Api.DAL.EF.Installers;
using FoodDelivery.Common.Extensions;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection___LocalDB")
    ?? throw new ArgumentException("The connection string is missing");

builder.Services.AddInstaller<ApiDALEFInstaller>(connectionString);
builder.Services.AddInstaller<ApiBLInstaller>();
builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        string? jwtSymmetricKey = builder.Configuration["JWT_SymmetricKey"];
        var key = Encoding.ASCII.GetBytes(jwtSymmetricKey
                                          ?? throw new ArgumentException("Symmetric key for JWT is missing"));

        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,

        };
    });

var app = builder.Build();

// ValidateAutoMapperConfiguration(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

static void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

// make the implicit Program class public so that test projects can access it
public partial class Program { }
