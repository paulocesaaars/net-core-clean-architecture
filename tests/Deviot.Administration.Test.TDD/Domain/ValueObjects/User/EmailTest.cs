using Deviot.Administration.Domain.Exceptions;
using Deviot.Administration.Domain.ValueObjects.User;
using FluentAssertions;
using System;
using Xunit;

namespace Deviot.Administration.Test.TDD.Domain.ValueObjects.User
{
    public class EmailTest
    {
        [Fact]
        public void CreateEmailSuccessfully()
        {
            var email = "paulo@teste.com.br";
            var result = new Email(email);

            result.Value.Should().Be(email);
        }

        [Fact]
        public void CreateEmailShoudExceptionEmailRequired()
        {
            Action act = () => new Email(string.Empty);
            act.Should().Throw<ObjectValidationException>().WithMessage("O email é obrigatório");
        }

        [Fact]
        public void CreateEmailShoudExceptionInvalidEmail()
        {
            Action act = () => new Email("email invalido");
            act.Should().Throw<ObjectValidationException>().WithMessage("O email é inválido (user@email.com)");
        }
    }
}
