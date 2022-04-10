using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace CarPool.DAL.Tests.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity EmptyRideEntity = new RideEntity(
        Id: default,
        StartLocation: default,
        EndLocation: default,
        StartTime: default,
        Duration: default,
        CarId: default,
        DriverId: default);

    public static readonly RideEntity RideEntity = new RideEntity(
        Id: Guid.Parse("99dc688e-f05b-4a5e-a306-fa447f0af6a2"),
        StartLocation: "Varsava",
        EndLocation: "Berlin",
        StartTime: new DateTime(year: 2022, month: 09, day: 09, hour: 08, minute: 30, second: 00),
        Duration: 4,
        CarId: CarSeeds.CarEntity.Id,
        DriverId: UserSeeds.UserEntity.Id);

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            RideEntity
        );
    }
}
