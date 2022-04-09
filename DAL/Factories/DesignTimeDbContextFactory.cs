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
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        builder.UseSqlite(
            connectionString: $"Data Source={Path.Join(path, "CarRide.db")}" //TODO Change this path to export the database file to the same folder as the binary
        );

        return new CarRideDbContext(builder.Options);
    }
}
