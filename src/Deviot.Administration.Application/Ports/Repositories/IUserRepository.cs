using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Administration.Domain.ValueObjects.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Administration.Application.Ports.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync(
            Pagination pagination,
            string fullname,
            string email
            );

        Task<User> FindByIdAsync(Id id);

        Task<User> FindByEmailAsync(Email email);

        Task<long> TotalRegistersAsync();

        Task InsertAsync(User user);

        Task UpdateAsync(User user);
    }
}
