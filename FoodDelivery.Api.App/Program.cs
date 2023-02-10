using System.Diagnostics;

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

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
