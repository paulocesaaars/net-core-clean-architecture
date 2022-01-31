using Deviot.Administration.Api.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Administration.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Dependency injection config
            services.AddDependencyInjectionConfiguration(Configuration);

            // Api - Configuration
            services.AddApiConfiguration(Configuration);

            // Versionamento
            services.AddVersioningConfiguration();

            // Swagger config
            services.AddSwaggerConfiguration();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(environment);
        }
    }
}
