using Deviot.Administration.Domain.Exceptions;
using Deviot.Administration.Domain.ValueObjects.Common;
using FluentAssertions;
using System;
using Xunit;

namespace Deviot.Administration.Test.TDD.Domain.ValueObjects.Common
{
    public class IdTest
    {
        [Fact]
        public void CreateIdSuccessfully()
        {
            var id = Guid.NewGuid();
            var result = new Id(id.ToString());

            result.Value.Should().Be(id);
        }

        [Fact]
        public void CreateIdShoudExceptionInvalidId()
        {
            Action act = () => new Id(string.Empty);
            act.Should().Throw<ObjectValidationException>().WithMessage("Id inválido");
        }
    }
}
