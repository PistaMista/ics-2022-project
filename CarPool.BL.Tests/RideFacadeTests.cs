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
    public sealed class RideFacadeTests : CrudFacadeTestsBase
    {
        private readonly RideFacade _rideFacadeSut;

        public RideFacadeTests(ITestOutputHelper output) : base(output)
        {
            _rideFacadeSut = new RideFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new RideModel
            (
                StartLocation: @"Praha",
                EndLocation: @"Brno",
                StartTime: new DateTime(2022,12, 12),
                Duration: 150,
                CarId: CarSeeds.CarEntity.Id,
                DriverId: UserSeeds.UserEntity.Id
            );

            var _ = await _rideFacadeSut.SaveAsync(model);
        }
    }
}
