﻿using AutoMapper;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Api.DAL.EF;
using FoodDelivery.Api.DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.Dal.IntegrationTests.Fixtures;
public class OrderRepositoryFixture : IDisposable, IDatabaseFixture
{
    public readonly OrderRepository repository;
    private readonly IMapper mapper;

    public OrderRepositoryFixture()
    {
        PrepareDatabase();
        repository = new OrderRepository(CreateDbContext(), mapper);
    }

    public IOrderRepository GetRepository()
    {
        return repository;
    }

    public void PrepareDatabase()
    {
        using (var dbContext = CreateDbContext())
        {
            dbContext.Database.EnsureCreated();
        }
    }

    public FoodDeliveryDbContext CreateDbContext()
    {
        var optionBuilder = new DbContextOptionsBuilder<FoodDeliveryDbContext>();
        optionBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodDelivery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        optionBuilder.EnableSensitiveDataLogging(true);
        var dbContext = new FoodDeliveryDbContext(optionBuilder.Options);
        return dbContext;
    }

    public void Dispose()
    {
        using (var dbContext = CreateDbContext())
        {
            dbContext.Database.EnsureDeleted();
            dbContext.SaveChanges();
        }
    }
}