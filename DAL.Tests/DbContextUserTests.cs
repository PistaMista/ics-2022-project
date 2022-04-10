using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Seeds;
using DAL.Entities;
using Common.Tests;

using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
       
    }

    [Fact]
    public async Task AddNew_UserWithoutCarsOrRides_Persisted()
    {
        var marie = UserSeeds.Marie with
        {
            Id = Guid.NewGuid()
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
        var jane = UserSeeds.Marie with
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            Cars = new List<CarEntity>()
        };

        var skoda = CarSeeds.Skoda with
        {
            Id = Guid.NewGuid()
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
        var lubomir = UserSeeds.Lubomir with
        {
            Id = Guid.NewGuid(),
            Cars = new List<CarEntity>(),
            RidesDriver = new List<RideEntity>()
        };

        var mercedes = CarSeeds.Mercedes with
        {
            Id = Guid.NewGuid()
        };

        var praha_brno = RideSeeds.praha_brno with
        {
            Id = Guid.NewGuid()
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
}
