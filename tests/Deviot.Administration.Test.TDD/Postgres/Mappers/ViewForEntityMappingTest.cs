using AutoMapper;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Test.Common.Fakes.Views;
using Deviot.Administration.Test.Common.Helpers;
using FluentAssertions;
using Xunit;

namespace Deviot.Administration.Test.TDD.Postgres.Mappers
{
    public class ViewForEntityMappingTest
    {
        public readonly IMapper _mapper = AutoMapperHelper.GetMapper();

        [Fact]
        public void ShouldReturnUserForUserViewModel()
        {
            var user = UserViewFake.GetUser1();

            var result = _mapper.Map<User>(user);

            result.Id.Value.Should().Be(user.Id);
            result.Email.Value.Should().Be(user.Email);
            result.FullName.Value.Should().Be(user.FullName);
        }
    }
}
