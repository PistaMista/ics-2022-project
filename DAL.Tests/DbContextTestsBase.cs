using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

using DAL.Tests.Factories;
namespace DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        //XUnitTestOutputConverter converter = new(output);
        //Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new SqliteDbContextTestingFactory(GetType().FullName!, seedTestingData: true);

        CarRideDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<CarRideDbContext> DbContextFactory { get; }
    protected CarRideDbContext CarRideDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await CarRideDbContextSUT.Database.EnsureDeletedAsync();
        await CarRideDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await CarRideDbContextSUT.Database.EnsureDeletedAsync();
        await CarRideDbContextSUT.DisposeAsync();
    }
}
