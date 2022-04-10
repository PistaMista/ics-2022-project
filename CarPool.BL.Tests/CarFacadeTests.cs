using CarPool.BL.Models;
using DAL;
using DAL.Factories;
using DAL.Seeds;
using System;
using System.Linq;
using System.Threading.Tasks;
using CarPool.BL.Facades;
using Common.Tests;
using Common.Enums;
using DAL.Entities;
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
                RegistrationDate: DateTime.MinValue
            );

            var _ = await _carFacadeSut.SaveAsync(model);
        }
    }
}
