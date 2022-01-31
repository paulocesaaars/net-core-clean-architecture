
using Deviot.Administration.Application.Base;
using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Domain.Entities;
using Deviot.Common;
using Microsoft.Extensions.Logging;

namespace Deviot.Administration.Application.UseCases
{
    public class FindAllUsersUseCase : UseCaseBase, IFindAllUsersUseCase
    {
        private IUserRepository _userRepository;

        private const string NO_CONTENT_MESSAGE = "Nenhum usuário foi encontrado";

        public FindAllUsersUseCase(
            INotifier notifier, 
            ILogger<FindAllUsersUseCase> logger,
            IUserRepository userRepository
            ) : base(notifier, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> ExecuteAsync(
            int pageNumber = 0, 
            int pageSize = 1000, 
            string fullname = "", 
            string email = ""
            )
        {
            try
            {
                var users = await _userRepository.FindAllAsync(
                    new Pagination(pageNumber, pageSize),
                    fullname,
                    email
                    );

                if (!users.Any())
                    NotifyNoContent(NO_CONTENT_MESSAGE);


                return users;
            }
            catch (Exception ex)
            {
                NotifyInternalServerError(ex);
                return null;
            }
        }
    }
}
