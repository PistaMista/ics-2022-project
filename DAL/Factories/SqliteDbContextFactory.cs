using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Factories;

public class SqliteDbContextFactory
{
    private readonly string _connectionString;
    private readonly bool _seedDemoData;

    public SqliteDbContextFactory(string connectionString, bool seedDemoData = false)
    {
        _connectionString = connectionString;
        _seedDemoData = seedDemoData;
    }

    public CarRideDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<CarRideDbContext>();
        optionsBuilder.UseSqlite(_connectionString);

        return new CarRideDbContext(optionsBuilder.Options, _seedDemoData);
    }
}
