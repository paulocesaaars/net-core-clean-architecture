using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Application.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Test.Common.Fakes.Entities;
using Deviot.Administration.Test.TDD.Base;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Deviot.Administration.Test.TDD.Application.UseCases
{
    public  class FindAllUsersUseCaseTest : UseCaseBaseTest
    {
        private readonly IFindAllUsersUseCase _findAllUsersUseCase;
        private readonly Mock<IUserRepository> _userRepository;

        public FindAllUsersUseCaseTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _findAllUsersUseCase = new FindAllUsersUseCase(
                _notifier,
                GetLogger<FindAllUsersUseCase>(), 
                _userRepository.Object
                );
        }

        [Fact(DisplayName = "Executa buscar todos usuários deve retornar usuários")]
        public async void ExecuteFindAllUsersUseCaseShoulReturnUsers()
        {
            var expected = UserFake.GetUsers();

            _userRepository.Setup(x => x.FindAllAsync(
                It.IsAny<Pagination>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).ReturnsAsync(expected);

            var result = await _findAllUsersUseCase.ExecuteAsync();

            _notifier.HasNotifications.Should().BeFalse();
            expected.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "Executa buscar todos usuários deve retornar lista vazia e 204")]
        public async void ExecuteFindAllUsersUseCaseShouldReturnEmptylistAndNoContent()
        {
            IEnumerable<User> expected = new List<User>();

            _userRepository.Setup(x => x.FindAllAsync(
                It.IsAny<Pagination>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).ReturnsAsync(expected);

            var result = await _findAllUsersUseCase.ExecuteAsync();
            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.NoContent);
            notification.Message.Should().Be("Nenhum usuário foi encontrado");

            expected.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "Executa buscar todos usuários deve retornar nulo, erro e 500")]
        public async void ExecuteFindAllUsersUseCaseShouldReturnNullAndInternalServerError()
        {
            _userRepository.Setup(x => x.FindAllAsync(
                It.IsAny<Pagination>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).ThrowsAsync(null);

            var result = await _findAllUsersUseCase.ExecuteAsync();
            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.InternalServerError);
            notification.Message.Should().Be(INTERNAL_ERROR_MESSAGE);

            result.Should().BeNull();
        }
    }
}
