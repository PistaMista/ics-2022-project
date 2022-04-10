using CarPool.Common.Enums;
using CarPool.Common.Tests;
using CarPool.DAL.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CarPool.DAL.Tests.Tests;

public class DbContextCarTests : DbContextTestsBase
{
    public DbContextCarTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task AddNew_CarWithoutOwner_Failed()
    {
        var entity = CarSeeds.EmptyCarEntity with
        {
            Manufacturer = "Fiat",
            Type = CarType.Limuzína
        };

        CarRideDbContextSUT.Add(entity);
        await Assert.ThrowsAsync<DbUpdateException>(
            async () => await CarRideDbContextSUT.SaveChangesAsync()
        );
    }

    [Fact]
    public async Task AddNew_CarWithOwner_Persisted()
    {
        var entity = CarSeeds.EmptyCarEntity with
        {
            Manufacturer = "Fiat",
            Type = CarType.Limuzína,
            SeatCount = 12,
            CarOwner = await CarRideDbContextSUT.Users.SingleAsync(u => u.Id == UserSeeds.UserEntity.Id)
        };

        CarRideDbContextSUT.Add(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Cars
            .Include(c => c.CarOwner)
            .SingleAsync(c => c.Id == entity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_Car()
    {
        var entity = await CarRideDbContextSUT.Cars.SingleAsync(c => c.Id == CarSeeds.CarEntity.Id);

        DeepAssert.Equal(
            expected:
                CarSeeds.CarEntity with
                {
                    CarOwner = null
                },
            actual:
                entity
        );
    }

    [Fact]
    public async Task Update_Car_Persisted()
    {
        var baseEntity = CarSeeds.CarEntity;
        var entity = baseEntity with
        {
            Manufacturer = baseEntity.Manufacturer + "os"
        };

        CarRideDbContextSUT.Cars.Update(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Cars.SingleAsync(c => c.Id == baseEntity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Car_Deleted()
    {
        var entity = CarSeeds.CarEntity;

        CarRideDbContextSUT.Cars.Remove(entity);
        await CarRideDbContextSUT.SaveChangesAsync();

        Assert.False(await CarRideDbContextSUT.Cars.AnyAsync(c => c.Id == entity.Id));
    }

    [Fact]
    public async Task DeleteById_Ride_Deleted()
    {
        var entity = CarSeeds.CarEntity;

        CarRideDbContextSUT.Cars.Remove(
            entity:
                CarRideDbContextSUT.Cars.Single(c => c.Id == entity.Id)
        );
        await CarRideDbContextSUT.SaveChangesAsync();

        Assert.False(await CarRideDbContextSUT.Cars.AnyAsync(r => r.Id == entity.Id));
    }
}
