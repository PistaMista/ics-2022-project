using CarPool.BL.Models;
using CarPool.DAL.Tests.Seeds;
using CarPool.BL.Facades;
using CarPool.Common.Tests;
using CarPool.Common.Enums;

using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;

namespace CarPool.BL.Tests
{
    public sealed class CarFacadeTests : CrudFacadeTestsBase
    {
        private readonly CarFacade _carFacadeSut;

        public CarFacadeTests(ITestOutputHelper output) : base(output)
        {
            _carFacadeSut = new CarFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new CarModel
            (
                Manufacturer: @"Subaru",
                Type: CarType.Offroad,
                LicensePlate: @"12345SPZ",
                SeatCount: 4,
                PhotoUrl: @"url",
                RegistrationDate: DateTime.MinValue,
                CarOwnerId: UserSeeds.UserEntity.Id
            );

            var _ = await _carFacadeSut.SaveAsync(model);
        }

        [Fact]
        public async Task GetAll_Single_SeededCar()
        {
            var cars = await _carFacadeSut.GetAsync();
            var car = cars.Single(i => i.Id == CarSeeds.CarEntity.Id);

            DeepAssert.Equal(Mapper.Map<CarInfoModel>(CarSeeds.CarEntity), car);
        }

        [Fact]
        public async Task GetById_SeededCar()
        {
            var car = await _carFacadeSut.GetAsync(CarSeeds.CarEntity.Id);

            DeepAssert.Equal(Mapper.Map<CarModel>(CarSeeds.CarEntity), car);
        }

        [Fact]
        public async Task GetById_NonExistent()
        {
            var car = await _carFacadeSut.GetAsync(Guid.Empty);

            Assert.Null(car);
        }

        [Fact]
        public async Task SeededCar_DeleteById_Deleted()
        {
            await _carFacadeSut.DeleteAsync(CarSeeds.CarEntity.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Cars.AnyAsync(i => i.Id == CarSeeds.CarEntity.Id));
        }

        [Fact]
        public async Task SeededCar_InsertOrUpdate_LicensePlateAndRegistrationDateUpdated()
        {
            //Arrange
            var car = new CarModel
            (
                Manufacturer: CarSeeds.CarEntity.Manufacturer,
                Type: CarSeeds.CarEntity.Type,
                LicensePlate: CarSeeds.CarEntity.LicensePlate,
                SeatCount: CarSeeds.CarEntity.SeatCount,
                PhotoUrl: CarSeeds.CarEntity.PhotoUrl,
                RegistrationDate: CarSeeds.CarEntity.RegistrationDate,
                CarOwnerId: CarSeeds.CarEntity.CarOwnerId
            )
            {
                Id = CarSeeds.CarEntity.Id
            };

            car.LicensePlate = "AAAA-02-03";
            car.RegistrationDate = new DateTime(year: 2016, month: 12, day: 01);

            //Act
            await _carFacadeSut.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Cars.SingleAsync(i => i.Id == car.Id);
            DeepAssert.Equal(car, Mapper.Map<CarModel>(carFromDb));
        }
    }
}
