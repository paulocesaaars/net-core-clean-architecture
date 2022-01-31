using Deviot.Administration.Domain.ValueObjects.Pagination;
using FluentAssertions;
using Xunit;

namespace Deviot.Administration.Test.TDD.Domain.ValueObjects.Pagination
{
    public class PageSizeTest
    {
        [Fact]
        public void CreatePageSizeMinValueSuccessfully()
        {
            var result = new PageSize(-5);
            result.Value.Should().Be(10);
        }

        [Fact]
        public void CreatePageSizeMaxValueSuccessfully()
        {
            var result = new PageSize(1000000);
            result.Value.Should().Be(1000);
        }
    }
}
