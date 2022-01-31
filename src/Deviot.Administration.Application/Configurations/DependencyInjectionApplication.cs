using Deviot.Administration.Application.Ports.UseCases;
using Deviot.Administration.Application.UseCases;
using Deviot.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Deviot.Administration.Application.Configurations
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddDependencyInjectionApplication(this IServiceCollection services)
        {
            // Common
            services.AddSingleton<INotifier, Notifier>();

            // Usecases
            services.AddSingleton<IFindAllUsersUseCase, FindAllUsersUseCase>();
            services.AddSingleton<IFindUserByIdUseCase, FindUserByIdUseCase>();
            services.AddSingleton<IAddUserUseCase, AddUserUseCase>();
            services.AddSingleton<IUpdateUserUseCase, UpdateUserUseCase>();

            return services;
        }
    }
}
