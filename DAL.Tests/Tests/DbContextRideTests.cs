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

namespace DAL.Tests.Tests;

public class DbContextRideTests : DbContextTestsBase
{
    public DbContextRideTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task AddNew_RideWithoutDriverAndCar_Failed()
    {
        var prahaBrno = RideSeeds.EmptyRideEntity with
        {
            StartLocation = "Praha",
            EndLocation = "Brno",
            StartTime = new DateTime(year: 2022, month: 12, day: 10, hour: 12, minute: 20, second: 2)
        };

        CarRideDbContextSUT.Add(prahaBrno);
        await Assert.ThrowsAsync<DbUpdateException>(
            async () => await CarRideDbContextSUT.SaveChangesAsync()
        );
    }

    [Fact]
    public async Task AddNew_RideWithoutDriver_Failed()
    {
        var prahaBrno = RideSeeds.EmptyRideEntity with
        {
            StartLocation = "Praha",
            EndLocation = "Brno",
            StartTime = new DateTime(year: 2022, month: 12, day: 10, hour: 12, minute: 20, second: 2),
            CarId = CarSeeds.CarEntity.Id
        };

        CarRideDbContextSUT.Add(prahaBrno);
        await Assert.ThrowsAsync<DbUpdateException>(
            async () => await CarRideDbContextSUT.SaveChangesAsync()
        );
    }

    [Fact]
    public async Task AddNew_RideWithoutCar_Failed()
    {
        var prahaBrno = RideSeeds.EmptyRideEntity with
        {
            StartLocation = "Praha",
            EndLocation = "Brno",
            StartTime = new DateTime(year: 2022, month: 12, day: 10, hour: 12, minute: 20, second: 2),
            DriverId = UserSeeds.UserEntity.Id
        };

        CarRideDbContextSUT.Add(prahaBrno);
        await Assert.ThrowsAsync<DbUpdateException>(
            async () => await CarRideDbContextSUT.SaveChangesAsync()
        );
    }

    [Fact]
    public async Task AddNew_RideWithoutPassengers_Persisted()
    {
        var prahaBrno = RideSeeds.EmptyRideEntity with
        {
            StartLocation = "Praha",
            EndLocation = "Brno",
            StartTime = new DateTime(year: 2022, month: 12, day: 10, hour: 12, minute: 20, second: 2),
            Driver = await CarRideDbContextSUT.Users.SingleAsync(x => x.Id == UserSeeds.UserEntity.Id),
            Car = await CarRideDbContextSUT.Cars.SingleAsync(x => x.Id == CarSeeds.CarEntity.Id)
        };

        CarRideDbContextSUT.Add(prahaBrno);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualPrahaBrno = await dbx.Rides
            .Include(r => r.Driver)
            .Include(r => r.Car)
            .SingleAsync(r => r.Id == prahaBrno.Id);

        DeepAssert.Equal(prahaBrno, actualPrahaBrno);
    }

    [Fact]
    public async Task AddNew_RideWithPassengers_Persisted()
    {
        var prahaBrno = RideSeeds.EmptyRideEntity with
        {
            StartLocation = "Praha",
            EndLocation = "Brno",
            StartTime = new DateTime(year: 2022, month: 12, day: 10, hour: 12, minute: 20, second: 2),
            Driver = await CarRideDbContextSUT.Users.SingleAsync(x => x.Id == UserSeeds.UserEntity.Id),
            Car = await CarRideDbContextSUT.Cars.SingleAsync(x => x.Id == CarSeeds.CarEntity.Id),
            Passengers = new List<UserEntity>()
            {
                await CarRideDbContextSUT.Users.SingleAsync(x => x.Id == UserSeeds.UserEntity2.Id)
            }
        };

        CarRideDbContextSUT.Add(prahaBrno);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualPrahaBrno = await dbx.Rides
            .Include(r => r.Driver)
            .Include(r => r.Car)
            .Include(r => r.Passengers)
            .SingleAsync(r => r.Id == prahaBrno.Id);

        DeepAssert.Equal(prahaBrno, actualPrahaBrno);
    }

    //[Fact]
    //public async Task GetById_User()
    //{
    //    var entity = await CarRideDbContextSUT.Users.SingleAsync(u => u.Id == UserSeeds.UserEntity.Id);

    //    DeepAssert.Equal(
    //        expected:
    //            UserSeeds.UserEntity with
    //            {
    //                RidesDriver = new List<RideEntity>(),
    //                RidesPassenger = new List<RideEntity>(),
    //                Cars = new List<CarEntity>()
    //            },
    //        actual:
    //            entity
    //    );
    //}

    //[Fact]
    //public async Task Update_User_Persisted()
    //{
    //    var baseEntity = UserSeeds.UserEntity;
    //    var entity = baseEntity with
    //    {
    //        FirstName = baseEntity.FirstName + "netta",
    //        LastName = baseEntity.LastName + "vaci"
    //    };

    //    CarRideDbContextSUT.Users.Update(entity);
    //    await CarRideDbContextSUT.SaveChangesAsync();

    //    await using var dbx = await DbContextFactory.CreateDbContextAsync();
    //    var actualEntity = await dbx.Users.SingleAsync(u => u.Id == baseEntity.Id);

    //    DeepAssert.Equal(entity, actualEntity);
    //}

    //[Fact]
    //public async Task Delete_User_Deleted()
    //{
    //    var entity = UserSeeds.UserEntity;

    //    CarRideDbContextSUT.Users.Remove(entity);
    //    await CarRideDbContextSUT.SaveChangesAsync();

    //    Assert.False(await CarRideDbContextSUT.Users.AnyAsync(u => u.Id == entity.Id));
    //}

    //[Fact]
    //public async Task DeleteById_User_Deleted()
    //{
    //    var entity = UserSeeds.UserEntity;

    //    CarRideDbContextSUT.Users.Remove(
    //        entity:
    //            CarRideDbContextSUT.Users.Single(u => u.Id == entity.Id)
    //    );
    //    await CarRideDbContextSUT.SaveChangesAsync();

    //    Assert.False(await CarRideDbContextSUT.Users.AnyAsync(u => u.Id == entity.Id));
    //}
}
