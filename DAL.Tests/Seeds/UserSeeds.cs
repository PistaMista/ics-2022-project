using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUserEntity = new UserEntity(
        Id: default,
        FirstName: default,
        LastName: default,
        PhotoUrl: default
        );

    public static readonly UserEntity UserEntity = new UserEntity(
        Id: Guid.Parse("260716c7-b16c-425d-8be1-550b1b12bf66"),
        FirstName: "Marie",
        LastName: "Testova",
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwp-media.familytoday.com%2F2016%2F04%2FfeaturedImageId23471.jpg&f=1&nofb=1"
        );

    static UserSeeds()
    {
        UserEntity.Cars.Add(CarSeeds.CarEntity);
        UserEntity.RidesDriver.Add(RideSeeds.RideEntity);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
           EmptyUserEntity,
           UserEntity
       );
    }
}
