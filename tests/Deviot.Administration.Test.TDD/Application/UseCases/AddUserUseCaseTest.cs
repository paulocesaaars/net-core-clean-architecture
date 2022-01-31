using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Application.UseCases;
using Deviot.Administration.Domain.Entities;
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
    public class AddUserUseCaseTest : UseCaseBaseTest
    {
        private readonly IAddUserUseCase _SaveUserUseCase;
        private readonly Mock<IUserRepository> _userRepository;

        public AddUserUseCaseTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _SaveUserUseCase = new AddUserUseCase(
                _notifier,
                GetLogger<AddUserUseCase>(), 
                _userRepository.Object
                );
        }

        [Fact(DisplayName = "Executa salvar usuário com sucesso")]
        public async void ExecuteSaveUserUseCaseWithSuccessfully()
        {
            var user = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByEmailAsync(It.IsAny<Email>())).ReturnsAsync(null as User);

            await _SaveUserUseCase.ExecuteAsync(user);

            var notification = _notifier.GetNotifications().First();
            var message = "Usuário adicionado com sucesso";

            notification.Type.Should().Be(HttpStatusCode.Created);
            notification.Message.Should().Be(message);
        }

        [Fact(DisplayName = "Executa salvar usuário deve retornar erro de validação email já existente e 400")]
        public async void ExecuteSaveUserUseCaseShouldReturnValidationErrorEmailIsUsedAnd400()
        {
            var user = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByEmailAsync(It.IsAny<Email>())).ReturnsAsync(user);

            await _SaveUserUseCase.ExecuteAsync(user);

            var notification = _notifier.GetNotifications().First();
            var message = String.Format("O email {0} já está sendo utilizado", user.Email.Value);

            notification.Type.Should().Be(HttpStatusCode.BadRequest);
            notification.Message.Should().Be(message);
        }


        [Fact(DisplayName = "Executa salvar usuário deve erro e 500")]
        public async void ExecuteSaveUserUseCaseShouldReturnAndInternalServerError()
        {
            var user = UserFake.GetUser1();

            _userRepository.Setup(x => x.FindByEmailAsync(It.IsAny<Email>())).ThrowsAsync(null);

            await _SaveUserUseCase.ExecuteAsync(user);

            var notification = _notifier.GetNotifications().First();

            notification.Type.Should().Be(HttpStatusCode.InternalServerError);
            notification.Message.Should().Be(INTERNAL_ERROR_MESSAGE);
        }
    }
}
