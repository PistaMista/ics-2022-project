using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Common.Enums;
using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPool.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity Lubomir = new UserEntity(
        Id: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b"),
        FirstName: "Lubomir",
        LastName: "Slanina",
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fsites.psu.edu%2Fsiowfa15%2Fwp-content%2Fuploads%2Fsites%2F29639%2F2015%2F10%2FBacon.jpg&f=1&nofb=1"
        );

    public static readonly UserEntity Marie = new UserEntity(
        Id: Guid.Parse("1dc05d92-5dde-42f6-82da-233abc47a056"),
        FirstName: "Marie",
        LastName: "Novakova",
        PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwp-media.familytoday.com%2F2016%2F04%2FfeaturedImageId23471.jpg&f=1&nofb=1"
        );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
           Lubomir,
           Marie
       );
    }
}
