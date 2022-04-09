using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Data.Sqlite;



namespace DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarRideDbContext>
{
    public CarRideDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<CarRideDbContext>();

        builder.UseSqlite(
            connectionString: @"Data Source=CarRide.db;"
        );

        return new CarRideDbContext(builder.Options);
    }
}
