using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Application.Ports.UseCases
{
    public interface IFindAllUsersUseCase
    {
        Task<IEnumerable<User>> ExecuteAsync(
            int pageNumber = 0,
            int pageSize = 1000,
            string fullname = "", 
            string email = ""
            );
    }
}
