using CarPool.BL.Models;
using CarPool.DAL.Tests.Seeds;
using CarPool.BL.Facades;
using CarPool.Common.Tests;

using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;

namespace CarPool.BL.Tests
{
    public sealed class UserFacadeTests : CrudFacadeTestsBase
    {
        private readonly UserFacade _userFacadeSut;

        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
            _userFacadeSut = new UserFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new UserModel
            (
                FirstName: @"Jan",
                LastName: @"Novotny",
                PhotoUrl: @"http://example.com/profile.jpg"
            );

            var _ = await _userFacadeSut.SaveAsync(model);
        }

        [Fact]
        public async Task GetAll_Single_SeededUser()
        {
            var users = await _userFacadeSut.GetAsync();
            var user = users.Single(i => i.Id == UserSeeds.UserEntity.Id);

            DeepAssert.Equal(Mapper.Map<UserInfoModel>(UserSeeds.UserEntity), user);
        }

        [Fact]
        public async Task GetById_SeededUser()
        {
            var user = await _userFacadeSut.GetAsync(UserSeeds.UserEntity2.Id);

            DeepAssert.Equal(Mapper.Map<UserModel>(UserSeeds.UserEntity2), user);
        }

        [Fact]
        public async Task GetById_NonExistent()
        {
            var user = await _userFacadeSut.GetAsync(Guid.Empty);

            Assert.Null(user);
        }

        [Fact]
        public async Task SeededUser_DeleteById_Deleted()
        {
            await _userFacadeSut.DeleteAsync(UserSeeds.UserEntity.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.UserEntity.Id));
        }

        [Fact]
        public async Task SeededUser_InsertOrUpdate_PhotoUrlUpdated()
        {
            //Arrange
            var user = new UserModel
            (
                FirstName: UserSeeds.UserEntity.FirstName,
                LastName: UserSeeds.UserEntity.LastName,
                PhotoUrl: UserSeeds.UserEntity.PhotoUrl
            )
            {
                Id = UserSeeds.UserEntity.Id
            };

            user.PhotoUrl = "http://example.com/new_profile.jpg";

            //Act
            await _userFacadeSut.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
            DeepAssert.Equal(user, Mapper.Map<UserModel>(userFromDb));
        }
    }
}
