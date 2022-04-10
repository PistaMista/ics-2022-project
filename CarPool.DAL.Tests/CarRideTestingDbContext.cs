using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarPool.DAL.Tests.Seeds;

namespace CarPool.DAL.Tests;

public class CarRideTestingDbContext : CarRideDbContext
{
    private readonly bool _seedTestingData;
    public CarRideTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false) : base(contextOptions, seedDemoData: false)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            UserSeeds.Seed(modelBuilder);
            CarSeeds.Seed(modelBuilder);
            RideSeeds.Seed(modelBuilder);
        }
    }
}
