using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Postgres.Builders;
using Deviot.Administration.Postgres.Configurations;
using Deviot.Administration.Postgres.Core;
using Deviot.Administration.Postgres.Repositories;
using Deviot.Administration.Test.Common.Fakes.Entities;
using Deviot.Administration.Test.Common.Helpers;
using Deviot.Common;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Administration.Test.TDD.Postgres.Repositories
{

    public class UserRepositoryTest
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            var config = new DbPostgresConfig
            {
                PostgresConnectionString = "Server=localhost;Database=administration;User Id=postgres;Password=paula@123",
                Schema = "public"
            };

            var dbService = new DbService(config);
            var mapper = AutoMapperHelper.GetMapper();
            var userBuilder = new UserBuilder(dbService);

            _userRepository = new UserRepository(dbService, mapper, userBuilder);
        }

        [Fact]
        public async Task FindAllAsync()
        {
            var result = await _userRepository.FindAllAsync(
                new Pagination(),
                string.Empty,
                string.Empty
                );

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task FindByIdAsync()
        {
            var id = UserFake.GetUser1().Id;
            var result = await _userRepository.FindByIdAsync(id);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task FindByEmailAsync()
        {
            var email = UserFake.GetUser1().Email;
            var result = await _userRepository.FindByEmailAsync(email);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task TotalRegistersAsync()
        {
            var result = await _userRepository.TotalRegistersAsync();

            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task InsertAsync()
        {
            var user = new User(
                Guid.NewGuid().ToString(),
                Utils.GenerateRandomString(10),
                $"{Utils.GenerateRandomString(5)}@teste.com"
                );

            Func<Task> act = _userRepository.Awaiting(x => x.InsertAsync(user));
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var user = UserFake.GetUser1();

            Func<Task> act = _userRepository.Awaiting(x => x.UpdateAsync(user));
            await act.Should().NotThrowAsync();
        }
    }
}
