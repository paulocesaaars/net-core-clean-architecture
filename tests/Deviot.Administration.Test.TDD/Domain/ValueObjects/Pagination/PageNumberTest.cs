using Deviot.Administration.Domain.ValueObjects.Pagination;
using FluentAssertions;
using Xunit;

namespace Deviot.Administration.Test.TDD.Domain.ValueObjects.Pagination
{
    public class PageNumberTest
    {
        [Fact]
        public void CreatePageNumberMinValueSuccessfully()
        {
            var result = new PageNumber(-10);
            result.Value.Should().Be(0);
        }
    }
}
