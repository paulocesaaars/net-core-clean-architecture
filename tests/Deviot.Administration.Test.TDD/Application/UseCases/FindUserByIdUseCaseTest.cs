using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Application.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Administration.Test.Common.Fakes.Entities;
using Deviot.Administration.Test.TDD.Base;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Net;
using Xunit;

namespace Deviot.Administration.Test.TDD.Application.UseCases
{
    public class FindUserByIdUseCaseTest : UseCaseBaseTest
    {
        private readonly IFindUserByIdUseCase _findUserByIdUseCase;
        private readonly Mock<IUserRepository> _userRepository;

        public FindUserByIdUseCaseTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _findUserByIdUseCase = new FindUserByIdUseCase(
                _notifier,
                GetLogger<FindUserByIdUseCase>(), 
                _userRepository.Object
                );
        }

        [Fact(DisplayName = "Executa buscar usuário por id deve retornar usuário")]
        public async void ExecuteFindUserByIdUseCaseShouldReturnUser()
        {
            var id = new Id(Guid.NewGuid().ToString());
            var expected = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ReturnsAsync(expected);

            var result = await _findUserByIdUseCase.ExecuteAsync(id);

            _notifier.HasNotifications.Should().BeFalse();
            expected.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "Executa buscar usuário por id deve retornar nulo, erro e 404")]
        public async void ExecuteFindUserByIdUseCaseShouldReturnNullAndNotFound()
        {
            var id = new Id(Guid.NewGuid().ToString());

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ReturnsAsync(null as User);

            var result = await _findUserByIdUseCase.ExecuteAsync(id);

            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.NotFound);
            notification.Message.Should().Be("O usuário não foi encontrado");

            result.Should().BeNull();
        }

        [Fact(DisplayName = "Executa buscar usuário por id deve retornar nulo, erro e 500")]
        public async void ExecuteFindUserByIdUseCaseShouldReturnNullAndInternalServerError()
        {
            var id = new Id(Guid.NewGuid().ToString());

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ThrowsAsync(null);

            var result = await _findUserByIdUseCase.ExecuteAsync(id);
            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.InternalServerError);
            notification.Message.Should().Be(INTERNAL_ERROR_MESSAGE);

            result.Should().BeNull();
        }
    }
}
