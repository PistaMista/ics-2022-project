using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity EmptyCarEntity = new(

        Id: default,
        Manufacturer: default,
        Type: default,
        LicensePlate: default,
        RegistrationDate: default,
        SeatCount: default,
        PhotoUrl: default,
        CarOwnerId: default);

    public static readonly CarEntity CarEntity = new(
        Id: Guid.Parse("2122ad6e-48ae-4230-a5f8-6a4b873d3168"),
        Manufacturer: "Mercedes",
        Type: CarType.ShootingBrake,
        LicensePlate: "FFFF-01-02",
        RegistrationDate: new DateTime(year: 2015, month: 12, day: 01),
        SeatCount: 4,
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.autocar.co.uk%2Fsites%2Fautocar.co.uk%2Ffiles%2Fimages%2Fcar-reviews%2Ffirst-drives%2Flegacy%2F1-mercedes-cla-shooting-brake-220d-2020-uk-fd-hero-front.jpg&f=1&nofb=1",
        CarOwnerId: UserSeeds.UserEntity.Id);


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            EmptyCarEntity,
            CarEntity
        );
    }
}
