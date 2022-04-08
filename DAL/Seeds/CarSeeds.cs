using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity Skoda = new(

        Id: Guid.Parse("3630f2eb-aaed-417b-b82f-de6aa2f5617c"),
        Manufacturer: "Skoda",
        Type: CarType.Sedan,
        LicensePlate: "DIKTAT0R",
        RegistrationDate: new DateTime(year: 2000, month: 03, day: 15),
        SeatCount: 4,
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.klassiekerweb.nl%2Fwp-content%2Fuploads%2F2014%2F04%2Fskoda_octavia_1959.jpg&f=1&nofb=1",
        CarOwnerId: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b"));

    public static readonly CarEntity Mercedes = new(

        Id: Guid.Parse("634cc2ac-4e65-4fdb-89dc-1c3c557dba78"),
        Manufacturer: "Mercedes",
        Type: CarType.ShootingBrake,
        LicensePlate: "FFFF-01-02",
        RegistrationDate: new DateTime(year: 2015, month: 12, day: 01),
        SeatCount: 4,
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.autocar.co.uk%2Fsites%2Fautocar.co.uk%2Ffiles%2Fimages%2Fcar-reviews%2Ffirst-drives%2Flegacy%2F1-mercedes-cla-shooting-brake-220d-2020-uk-fd-hero-front.jpg&f=1&nofb=1",
        CarOwnerId: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b"));


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Skoda,
            Mercedes
        );
    }
}
