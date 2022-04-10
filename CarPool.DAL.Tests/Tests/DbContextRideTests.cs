using CarPool.Common.Tests;
using CarPool.DAL.Tests.Seeds;
using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CarPool.DAL.Tests.Tests;

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

    [Fact]
    public async Task GetById_Ride()
    {
        var entity = await CarRideDbContextSUT.Rides.SingleAsync(u => u.Id == RideSeeds.RideEntity.Id);

        DeepAssert.Equal(
            expected:
                RideSeeds.RideEntity with
                {
                    Car = null,
                    Driver = null,
                    Passengers = new List<UserEntity>()
                },
            actual:
                entity
        );
    }

    [Fact]
    public async Task Update_Ride_Persisted()
    {
        var baseEntity = RideSeeds.RideEntity;
        var entity = baseEntity with
        {
            StartLocation = baseEntity + " 2",
            EndLocation = baseEntity + "pe"
        };

        CarRideDbContextSUT.Rides.Update(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Rides.SingleAsync(r => r.Id == baseEntity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Ride_Deleted()
    {
        var entity = RideSeeds.RideEntity;

        CarRideDbContextSUT.Rides.Remove(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        Assert.False(await CarRideDbContextSUT.Rides.AnyAsync(r => r.Id == entity.Id));
    }

    [Fact]
    public async Task DeleteById_Ride_Deleted()
    {
        var entity = RideSeeds.RideEntity;

        CarRideDbContextSUT.Rides.Remove(
            entity:
                CarRideDbContextSUT.Rides.Single(r => r.Id == entity.Id)
        );
        await CarRideDbContextSUT.SaveChangesAsync();

        Assert.False(await CarRideDbContextSUT.Rides.AnyAsync(r => r.Id == entity.Id));
    }
}
