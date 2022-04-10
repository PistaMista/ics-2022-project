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

        [Fact]
        public async Task GetAll_Single_SeededRide()
        {
            var rides = await _rideFacadeSut.GetAsync();
            var ride = rides.Single(i => i.Id == RideSeeds.RideEntity.Id);

            DeepAssert.Equal(Mapper.Map<RideInfoModel>(RideSeeds.RideEntity), ride);
        }
        [Fact]
        public async Task GetById_SeededRide()
        {
            var ride = await _rideFacadeSut.GetAsync(RideSeeds.RideEntity.Id);

            DeepAssert.Equal(Mapper.Map<RideModel>(RideSeeds.RideEntity), ride);
        }
        [Fact]
        public async Task GetById_NonExistent()
        {
            var ride = await _rideFacadeSut.GetAsync(Guid.Empty);

            Assert.Null(ride);
        }
        [Fact]
        public async Task SeededRide_DeleteById_Deleted()
        {
            await _rideFacadeSut.DeleteAsync(RideSeeds.RideEntity.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Rides.AnyAsync(i => i.Id == RideSeeds.RideEntity.Id));
        }
        [Fact]
        public async Task SeededRide_InsertOrUpdate_RideUpdated()
        {
            //Arrange
            var ride = new RideModel
            (
                StartLocation: @"Praha",
                EndLocation: @"Brno",
                StartTime: new DateTime(2022, 12, 12),
                Duration: 150,
                CarId: CarSeeds.CarEntity.Id,
                DriverId: UserSeeds.UserEntity.Id
            )
            {
                Id = RideSeeds.RideEntity.Id
            };

            ride.EndLocation = "Varsava";
            ride.StartTime = new DateTime(year: 2023, month: 12, day: 01);

            //Act
            await _rideFacadeSut.SaveAsync(ride);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var rideFromDb = await dbxAssert.Rides.SingleAsync(i => i.Id == ride.Id);
            DeepAssert.Equal(ride, Mapper.Map<RideModel>(rideFromDb));
        }
    }
}
