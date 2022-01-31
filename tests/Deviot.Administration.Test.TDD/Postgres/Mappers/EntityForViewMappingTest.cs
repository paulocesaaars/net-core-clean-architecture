using AutoMapper;
using Deviot.Administration.Postgres.Views;
using Deviot.Administration.Test.Common.Fakes.Entities;
using Deviot.Administration.Test.Common.Helpers;
using FluentAssertions;
using Xunit;

namespace Deviot.Administration.Test.TDD.Postgres.Mappers
{
    public class EntityForViewMappingTest
    {
        public readonly IMapper _mapper = AutoMapperHelper.GetMapper();

        [Fact]
        public void ShouldReturnUserForUserViewModel()
        {
            var user = UserFake.GetUser1();

            var result = _mapper.Map<UserView>(user);

            result.Id.Should().Be(user.Id.Value);
            result.Email.Should().Be(user.Email.Value);
            result.FullName.Should().Be(user.FullName.Value);
        }
    }
}
