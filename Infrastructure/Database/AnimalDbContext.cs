using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Domain.Models.Animal;

namespace Domain.Data
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<AnimalModel> animalModels { get; set; }
        public DbSet<Bird> Birds { get; set; }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Cat> Cats { get; set; }



        private readonly IConfiguration _configuration;

        public AnimalDbContext(DbContextOptions<AnimalDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Clean-Api;Trusted_Connection=True;TrustServerCertificate=Yes");

        }
    }
}