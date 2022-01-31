using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Postgres.Builders;
using Deviot.Administration.Postgres.Core;
using Deviot.Administration.Postgres.Mapppings;
using Deviot.Administration.Postgres.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Deviot.Administration.Postgres.Configurations
{
    public static class DependencyInjectionPostgres
    {
        public static IServiceCollection AddDependencyInjectionPostgres(this IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(typeof(EntityForViewMapping),
                                   typeof(ViewForEntityMapping));

            // Repositories
            services.AddSingleton<IUserRepository, UserRepository>();

            // Builders
            services.AddSingleton<IUserBuilder, UserBuilder>();

            // Services
            services.AddSingleton<IDbService, DbService>();

            return services;
        }
    }
}
