
using Deviot.Administration.Application.Base;
using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Common;
using Microsoft.Extensions.Logging;

namespace Deviot.Administration.Application.UseCases
{
    public class AddUserUseCase : UseCaseBase, IAddUserUseCase
    {
        private IUserRepository _userRepository;

        private const string USER_CREATED_MESSAGE = "Usuário adicionado com sucesso";
        private const string EMAIL_IS_USED_MESSAGE = "O email {0} já está sendo utilizado";

        public AddUserUseCase(
            INotifier notifier, 
            ILogger<AddUserUseCase> logger,
            IUserRepository userRepository
            ) : base(notifier, logger)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(User user)
        {
            try
            {
                var result = await _userRepository.FindByEmailAsync(user.Email);

                if (result is not null)
                {
                    NotifyBadRequest(String.Format(EMAIL_IS_USED_MESSAGE, user.Email.Value));
                    return;
                }
                    

                await _userRepository.InsertAsync(user);
                NotifyCreated(USER_CREATED_MESSAGE);
            }
            catch (Exception ex)
            {
                NotifyInternalServerError(ex);
            }
        }
    }
}
