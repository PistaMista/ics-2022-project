using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Seeds;

using Microsoft.EntityFrameworkCore;

namespace DAL;

public class CarRideDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public CarRideDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : base(contextOptions)
    {
        _seedDemoData = seedDemoData;
    }

    public DbSet<CarEntity> IngredientAmountEntities => Set<CarEntity>();
    public DbSet<UserEntity> Recipes => Set<UserEntity>();
    public DbSet<RideEntity> Ingredients => Set<RideEntity>();

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    if (_seedDemoData)
    //    {
    //        CarSeeds.Seed(modelBuilder);
    //        RideSeeds.Seed(modelBuilder);
    //        UserSeeds.Seed(modelBuilder);
    //    }
    //}
}
