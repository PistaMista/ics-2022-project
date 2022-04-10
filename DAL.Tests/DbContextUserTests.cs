using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Seeds;
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
        var lubomir = UserSeeds.Lubomir;

        CarRideDbContextSUT.Add(lubomir);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualLubomir = await dbx.Users.SingleAsync(u => u.Id == lubomir.Id);

        DeepAssert.Equal(lubomir, actualLubomir);
    }

    [Fact]
    public async Task AddNew_UserWithCar_Persisted()
    {
        var jane = UserSeeds.Marie with
        {
            FirstName = "Jane"
        };

        var skoda = CarSeeds.Skoda;
        jane.Cars.Add(skoda);

        CarRideDbContextSUT.Add(jane);
        await CarRideDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualJane = await dbx.Users
            .Include(u => u.Cars)
            .SingleAsync(u => u.Id == jane.Id);

        DeepAssert.Equal(jane, actualJane);
    }
}
