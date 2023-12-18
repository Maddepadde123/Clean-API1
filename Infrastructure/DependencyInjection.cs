using Domain.Data;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.AnimalRepository;
using Infrastructure.Repositories.AnimalUserRepository;
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

            services.AddScoped<IUserRepository, UserRepository>(); // Byt ut UserRepository mot den faktiska implementationen
            services.AddScoped<IAnimalRepository, AnimalRepository>(); // För AnimalRepository
            services.AddScoped<IAnimalUserRepository, AnimalUserRepository>();
            services.AddScoped<Authentication.Authentication>();

            return services;
        }

    }
}
