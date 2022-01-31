using Deviot.Administration.Domain.Exceptions;
using Deviot.Administration.Domain.ValueObjects.User;
using Deviot.Common;
using FluentAssertions;
using System;
using Xunit;

namespace Deviot.Administration.Test.TDD.Domain.ValueObjects.User
{
    public class FullNameTest
    {
        [Fact]
        public void CreatefullnameSuccessfully()
        {
            var fullname = "Paulo César de Souza";
            var result = new FullName(fullname);

            result.Value.Should().Be(fullname);
        }

        [Fact]
        public void CreatefullnameShoudExceptionLengthMin()
        {
            Action act = () => new FullName(string.Empty);
            act.Should().Throw<ObjectValidationException>().WithMessage("O nome completo deve ter no mínimo 5 caracteres");
        }

        [Fact]
        public void CreatefullnameShoudExceptionLengthMax()
        {
            Action act = () => new FullName(Utils.GenerateRandomString(200));
            act.Should().Throw<ObjectValidationException>().WithMessage("O nome completo deve ter no máximo 100 caracteres");
        }
    }
}
