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

        public PassengerFacadeTests(ITestOutputHelper output) : base(output)
        {
            _passengerFacadeSUT = new PassengerFacade(
                new UserFacade(UnitOfWorkFactory, Mapper),
                new RideFacade(UnitOfWorkFactory, Mapper),
                Mapper);
        }

       

        [Fact]
        public async Task AddUserToRide_Persisted()
        {
            await _passengerFacadeSUT.AddUserToRide(UserSeeds.UserEntity.Id, RideSeeds.RideEntity.Id);
        }
    }
}
