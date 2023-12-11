using Domain.Data;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<AnimalDbContext>(options =>
            {
                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Clean-Api;Trusted_Connection=True;TrustServerCertificate=Yes";
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
