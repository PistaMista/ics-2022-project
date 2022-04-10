using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Tests.Seeds;
using DAL.Entities;
using Common.Tests;

using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task AddNew_UserWithoutCarsOrRides_Persisted()
    {
        var marie = UserSeeds.EmptyUserEntity with
        {
            FirstName = "Marie",
            LastName = "Testova"
        };

        CarRideDbContextSUT.Add(marie);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualMarie = await dbx.Users.SingleAsync(u => u.Id == marie.Id);

        DeepAssert.Equal(marie, actualMarie);
    }

    [Fact]
    public async Task AddNew_UserWithCar_Persisted()
    {
        var jane = UserSeeds.EmptyUserEntity with
        {
            FirstName = "Jane",
            LastName = "Novakova",
            Cars = new List<CarEntity>()
        };

        var skoda = CarSeeds.EmptyCarEntity with
        {
            Manufacturer = "Skoda",
            Type = Common.Enums.CarType.Kombi
        };

        jane.Cars.Add(skoda);

        CarRideDbContextSUT.Add(jane);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualJane = await dbx.Users
            .Include(u => u.Cars)
            .SingleAsync(u => u.Id == jane.Id);

        DeepAssert.Equal(jane, actualJane);
    }

    [Fact]
    public async Task AddNew_Driver_Persisted()
    {
        var lubomir = UserSeeds.EmptyUserEntity with
        {
            FirstName = "Lubomir",
            LastName = "Slanina",
            Cars = new List<CarEntity>(),
            RidesDriver = new List<RideEntity>()
        };

        var mercedes = CarSeeds.EmptyCarEntity with
        {
            Manufacturer = "Mercedes",
            Type = Common.Enums.CarType.Kabriolet
        };

        var praha_brno = RideSeeds.EmptyRideEntity with
        {
            StartLocation = "Praha",
            EndLocation = "Brno",
            Car = mercedes
        };

        lubomir.Cars.Add(mercedes);
        lubomir.RidesDriver.Add(praha_brno);

        CarRideDbContextSUT.Add(lubomir);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualLubomir = await dbx.Users
            .Include(u => u.Cars)
            .Include(u => u.RidesDriver)
            .SingleAsync(u => u.Id == lubomir.Id);

        DeepAssert.Equal(lubomir, actualLubomir);
    }

    [Fact]
    public async Task GetById_User()
    {
        var entity = await CarRideDbContextSUT.Users.SingleAsync(u => u.Id == UserSeeds.UserEntity.Id);

        DeepAssert.Equal(
            expected:
                UserSeeds.UserEntity with
                {
                    RidesDriver = new List<RideEntity>(),
                    RidesPassenger = new List<RideEntity>(),
                    Cars = new List<CarEntity>()
                },
            actual:
                entity
        );
    }

    [Fact]
    public async Task Update_User_Persisted()
    {
        var baseEntity = UserSeeds.UserEntity;
        var entity = baseEntity with
        {
            FirstName = baseEntity.FirstName + "netta",
            LastName = baseEntity.LastName + "vaci"
        };

        CarRideDbContextSUT.Users.Update(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleAsync(u => u.Id == baseEntity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_User_Deleted()
    {
        var entity = UserSeeds.UserEntity;

        CarRideDbContextSUT.Users.Remove(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        Assert.False(await CarRideDbContextSUT.Users.AnyAsync(u => u.Id == entity.Id));
    }

    [Fact]
    public async Task DeleteById_User_Deleted()
    {
        var entity = UserSeeds.UserEntity;

        CarRideDbContextSUT.Users.Remove(
            entity:
                CarRideDbContextSUT.Users.Single(u => u.Id == entity.Id)
        );
        await CarRideDbContextSUT.SaveChangesAsync();

        Assert.False(await CarRideDbContextSUT.Users.AnyAsync(u => u.Id == entity.Id));
    }
}
