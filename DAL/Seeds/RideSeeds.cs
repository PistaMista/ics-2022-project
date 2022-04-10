using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity praha_brno = new RideEntity(
        Id: Guid.Parse("cdaf1c5d-d1c8-45a8-9fb3-9c0f1d7c19f2"),
        StartLocation: "Praha",
        EndLocation: "Brno",
        StartTime: new DateTime(year: 2022, month: 05, day: 28, hour: 14, minute: 30, second: 00),
        Duration: 4,
        CarId: Guid.Parse("634cc2ac-4e65-4fdb-89dc-1c3c557dba78"),
        DriverId: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b"));

    public static readonly RideEntity varsava_berlin = new RideEntity(
        Id: Guid.Parse("99dc688e-f05b-4a5e-a306-fa447f0af6a2"),
        StartLocation: "Varsava",
        EndLocation: "Berlin",
        StartTime: new DateTime(year: 2022, month: 09, day: 09, hour: 08, minute: 30, second: 00),
        Duration: 4,
        CarId: Guid.Parse("3630f2eb-aaed-417b-b82f-de6aa2f5617c"),
        DriverId: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b"));

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            praha_brno,
            varsava_berlin
        );
    }
}
