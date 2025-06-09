using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ucondo_challenge.infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UCONDO_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not set in the environment variables.");
            }

            services.AddDbContext<UCondoChallengeDbContext>(options =>
                options.UseNpgsql(connectionString));

        }
    }
}
