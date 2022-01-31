
using Deviot.Administration.Application.Base;
using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Common;
using Microsoft.Extensions.Logging;

namespace Deviot.Administration.Application.UseCases
{
    public class UpdateUserUseCase : UseCaseBase, IUpdateUserUseCase
    {
        private IUserRepository _userRepository;

        private const string USER_UPDATED_MESSAGE = "Usuário atualizado com sucesso";
        private const string NOT_FOUND_MESSAGE = "O usuário não foi encontrado";
        private const string EMAIL_IS_USED_MESSAGE = "O email {0} já está sendo utilizado";

        public UpdateUserUseCase(
            INotifier notifier, 
            ILogger<UpdateUserUseCase> logger,
            IUserRepository userRepository
            ) : base(notifier, logger)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(User user)
        {
            try
            {
                var currentUser = await _userRepository.FindByIdAsync(user.Id);
                
                if(currentUser is null)
                {
                    NotifyNotFound(NOT_FOUND_MESSAGE);
                    return;
                }
                    

                var emailUser = await _userRepository.FindByEmailAsync(user.Email);

                if (emailUser is not null)
                {
                    if (user.Id.Value != emailUser.Id.Value)
                    {
                        NotifyBadRequest(String.Format(EMAIL_IS_USED_MESSAGE, user.Email.Value));
                        return;
                    }
                        
                }

                currentUser.Setfullname(user.FullName);
                currentUser.SetEmail(user.Email);

                await _userRepository.UpdateAsync(currentUser);
                NotifyOk(USER_UPDATED_MESSAGE);
            }
            catch (Exception ex)
            {
                NotifyInternalServerError(ex);
            }
        }
    }
}
