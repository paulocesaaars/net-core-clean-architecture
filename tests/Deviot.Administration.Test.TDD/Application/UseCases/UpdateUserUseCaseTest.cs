using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Application.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Administration.Domain.ValueObjects.User;
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
    public class UpdateUserUseCaseTest : UseCaseBaseTest
    {
        private readonly IUpdateUserUseCase _UpdateUserUseCase;
        private readonly Mock<IUserRepository> _userRepository;

        public UpdateUserUseCaseTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _UpdateUserUseCase = new UpdateUserUseCase(
                _notifier,
                GetLogger<UpdateUserUseCase>(), 
                _userRepository.Object
                );
        }

        [Fact(DisplayName = "Executa atualizar usuário com sucesso")]
        public async void ExecuteUpdateUserUseCaseWithSuccessfully()
        {
            var user = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ReturnsAsync(user);
            _userRepository.Setup(x => x.FindByEmailAsync(It.IsAny<Email>())).ReturnsAsync(null as User);

            await _UpdateUserUseCase.ExecuteAsync(user);

            var notification = _notifier.GetNotifications().First();
            var message = "Usuário atualizado com sucesso";

            notification.Type.Should().Be(HttpStatusCode.OK);
            notification.Message.Should().Be(message);
        }

        [Fact(DisplayName = "Executa atualizar usuário deve retornar erro de validação usuário não encontrado")]
        public async void ExecuteUpdateUserUseCaseShouldReturnValidationErrorNotFoundUserAnd404()
        {
            var user = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ReturnsAsync(null as User);

            await _UpdateUserUseCase.ExecuteAsync(user);

            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.NotFound);
            notification.Message.Should().Be("O usuário não foi encontrado");
        }

        [Fact(DisplayName = "Executa atualizar usuário deve retornar erro de validação email já existente e 400")]
        public async void ExecuteUpdateUserUseCaseShouldReturnValidationErrorEmailIsUsedAnd400()
        {
            var user1 = UserFake.GetUser1();
            var user2 = UserFake.GetUser2();

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ReturnsAsync(user1);
            _userRepository.Setup(x => x.FindByEmailAsync(It.IsAny<Email>())).ReturnsAsync(user2);

            await _UpdateUserUseCase.ExecuteAsync(user1);

            var notification = _notifier.GetNotifications().First();
            var message = String.Format("O email {0} já está sendo utilizado", user1.Email.Value);

            notification.Type.Should().Be(HttpStatusCode.BadRequest);
            notification.Message.Should().Be(message);
        }


        [Fact(DisplayName = "Executa atualizar usuário deve retornar erro e 500")]
        public async void ExecuteUpdateUserUseCaseShouldReturnInternalServerError()
        {
            var user = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByIdAsync(It.IsAny<Id>())).ThrowsAsync(null);

            await _UpdateUserUseCase.ExecuteAsync(user);

            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.InternalServerError);
            notification.Message.Should().Be(INTERNAL_ERROR_MESSAGE);
        }
    }
}
