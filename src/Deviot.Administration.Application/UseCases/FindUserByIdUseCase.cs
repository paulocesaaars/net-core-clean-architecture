
using Deviot.Administration.Application.Base;
using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Common;
using Microsoft.Extensions.Logging;

namespace Deviot.Administration.Application.UseCases
{
    public class FindUserByIdUseCase : UseCaseBase, IFindUserByIdUseCase
    {
        private IUserRepository _userRepository;

        private const string NOT_FOUND_MESSAGE = "O usuário não foi encontrado";

        public FindUserByIdUseCase(
            INotifier notifier, 
            ILogger<FindUserByIdUseCase> logger,
            IUserRepository userRepository
            ) : base(notifier, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<User> ExecuteAsync(Id id)
        {
            try
            {
                var result = await _userRepository.FindByIdAsync(id);

                if (result is null)
                    NotifyNotFound(NOT_FOUND_MESSAGE);

                return result;
            }
            catch (Exception ex)
            {
                NotifyInternalServerError(ex);
            }

            return null;
        }
    }
}
