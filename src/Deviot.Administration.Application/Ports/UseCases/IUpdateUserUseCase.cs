using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Application.Ports.UseCases
{
    public interface IUpdateUserUseCase
    {
        Task ExecuteAsync(User user);
    }
}
