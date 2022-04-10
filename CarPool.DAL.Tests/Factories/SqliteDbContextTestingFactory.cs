using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CarPool.DAL.Tests.Factories;

public class SqliteDbContextTestingFactory : IDbContextFactory<CarRideDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public SqliteDbContextTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public CarRideDbContext CreateDbContext()
    {
        var builder = new DbContextOptionsBuilder<CarRideDbContext>();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        return new CarRideTestingDbContext(builder.Options, _seedTestingData);
    }
}
