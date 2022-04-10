﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CarPool.Common.Tests;
//using CarPool.Common.Tests.Factories;
using CarPool.DAL;
using CarPool.DAL.Factories;
using CarPool.DAL.Tests.Factories;
using CarPool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CarPool.BL.Tests;

public class  CrudFacadeTestsBase : IAsyncLifetime
{
    protected CrudFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        //DbContextFactory = new DbContextInMemoryFactory(GetType().Name, seedTestingData: true);
        DbContextFactory = new SqliteDbContextTestingFactory(GetType().FullName!, seedTestingData: true);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);

        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    typeof(BusinessLogic),
                });
                cfg.AddCollectionMappers();

                using var dbContext = DbContextFactory.CreateDbContext();
                cfg.UseEntityFrameworkCoreModel<CarRideDbContext>(dbContext.Model);
            }
        );
        Mapper = new Mapper(configuration);
        Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    protected IDbContextFactory<CarRideDbContext> DbContextFactory { get; }

    protected Mapper Mapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
