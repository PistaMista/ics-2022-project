using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.DAL.Entities;
using CarPool.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarPool.DAL.Tests.Seeds;

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

    public static readonly UserEntity UserEntity2 = new UserEntity(
        Id: Guid.Parse("6381cda0-6d59-45f1-a425-376d3ae2586c"),
        FirstName: "Pasek",
        LastName: "Pasazer",
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwp-media.familytoday.com%2F2016%2F04%2FfeaturedImageId23471.jpg&f=1&nofb=1"
        );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
           UserEntity,
           UserEntity2
       );
    }
}
