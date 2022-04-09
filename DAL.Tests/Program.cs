using DAL;
using DAL.Entities;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlite();

            using (var ctx = new CarRideDbContext(builder.Options))
            {
                CarEntity Skoda = new(
                    Id: Guid.Parse("3630f2eb-aaed-417b-b82f-de6aa2f5617c"),
                    Manufacturer: "Skoda",
                    Type: CarType.Sedan,
                    LicensePlate: "DIKTAT0R",
                    RegistrationDate: new DateTime(year: 2000, month: 03, day: 15),
                    SeatCount: 4,
                    PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.klassiekerweb.nl%2Fwp-content%2Fuploads%2F2014%2F04%2Fskoda_octavia_1959.jpg&f=1&nofb=1",
                    CarOwnerId: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b")
                );

                ctx.Cars.Add(Skoda);

                var Lubomir = new UserEntity(
                    Id: Guid.Parse("6860fad0-cd02-47b7-af5d-194288d2947b"),
                    FirstName: "Lubomir",
                    LastName: "Slanina",
                    PhotoUrl: @"https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fsites.psu.edu%2Fsiowfa15%2Fwp-content%2Fuploads%2Fsites%2F29639%2F2015%2F10%2FBacon.jpg&f=1&nofb=1"
                 );

                ctx.Users.Add(Lubomir);
                ctx.SaveChanges();
            }
        }
    }
}
