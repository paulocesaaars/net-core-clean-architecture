using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Application.Ports.UseCases
{
    public interface IAddUserUseCase
    {
        Task ExecuteAsync(User user);
    }
}
