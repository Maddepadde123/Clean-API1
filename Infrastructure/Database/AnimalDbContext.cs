﻿using Domain.Models;
using Domain.Models.Animal;
using Domain.Models.AnimalUser;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Data
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<AnimalModel> AnimalModels { get; set; }
        public DbSet<Bird> Birds { get; set; }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Cat> Cats { get; set; }

        public DbSet<UserModel> UserModel { get; set; }

        public DbSet<AnimalUserModel> AnimalUserModels { get; set; }



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