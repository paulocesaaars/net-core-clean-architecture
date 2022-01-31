using Deviot.Administration.Api.Mapppings;
using Deviot.Administration.Application.Configurations;
using Deviot.Administration.Postgres.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Administration.Api.Configurations
{
    [ExcludeFromCodeCoverage]

    public static class DependencyInjectionConfig
    {
        private static string POSTGRES_SETTINGS = "PostgresSettings";

        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Postgres
            var postgresSettings = new DbPostgresConfig();
            configuration.Bind(POSTGRES_SETTINGS, postgresSettings);
            services.AddSingleton(postgresSettings);

            // Automapper
            services.AddAutoMapper(typeof(EntityToViewModelMapping),
                                   typeof(ViewModelToEntityMapping));

            // Dependency Injections
            services.AddDependencyInjectionApplication();

            // Dependency Injections Adapters
            services.AddDependencyInjectionPostgres();

            return services;
        }
    }
}
