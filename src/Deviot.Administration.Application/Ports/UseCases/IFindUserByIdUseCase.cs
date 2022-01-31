using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.ValueObjects.Common;

namespace Deviot.Administration.Application.Ports.UseCases
{
    public interface IFindUserByIdUseCase
    {
        Task<User> ExecuteAsync(Id id);
    }
}
