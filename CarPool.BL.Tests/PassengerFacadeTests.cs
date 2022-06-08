using CarPool.BL.Models;
using CarPool.DAL;
using CarPool.DAL.Factories;
using CarPool.DAL.Tests.Seeds;
using System;
using System.Linq;
using System.Threading.Tasks;
using CarPool.BL.Facades;
using CarPool.Common.Tests;
using CarPool.Common.Enums;
using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CarPool.BL.Tests
{
    public sealed class PassengerFacadeTests : CrudFacadeTestsBase
    {
        private readonly PassengerFacade _passengerFacadeSUT;
        private readonly RideFacade _rideFacadeSUT;

        public PassengerFacadeTests(ITestOutputHelper output) : base(output)
        {
            _rideFacadeSUT = new RideFacade(UnitOfWorkFactory, Mapper);
            _passengerFacadeSUT = new PassengerFacade(
                new UserFacade(UnitOfWorkFactory, Mapper),
                _rideFacadeSUT,
                Mapper);
        }

       

        [Fact]
        public async Task AddUserToRide_Persisted()
        {
            await _passengerFacadeSUT.AddPassengerToRide(UserSeeds.UserEntity2.Id, RideSeeds.RideEntity.Id);

            RideModel ride = await _rideFacadeSUT.GetAsync(RideSeeds.RideEntity.Id);
            UserModel addedPassenger = ride.Passengers.SingleOrDefault(x => x.Id == UserSeeds.UserEntity2.Id);

            Assert.NotNull(addedPassenger);
        }
    }
}
