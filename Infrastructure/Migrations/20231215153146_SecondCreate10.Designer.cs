﻿// <auto-generated />
using System;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AnimalDbContext))]
    [Migration("20231215153146_SecondCreate10")]
    partial class SecondCreate10
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Animal.AnimalModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AnimalModels");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AnimalModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.AnimalUser.AnimalUserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("UserId");

                    b.ToTable("AnimalUserModels");
                });

            modelBuilder.Entity("Domain.Models.User.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserModel");
                });

            modelBuilder.Entity("Domain.Models.Bird", b =>
                {
                    b.HasBaseType("Domain.Models.Animal.AnimalModel");

                    b.Property<bool>("CanFly")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Bird");
                });

            modelBuilder.Entity("Domain.Models.Cat", b =>
                {
                    b.HasBaseType("Domain.Models.Animal.AnimalModel");

                    b.Property<string>("CatBreed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CatWeight")
                        .HasColumnType("int");

                    b.Property<bool>("LikesToPlay")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Cat");
                });

            modelBuilder.Entity("Domain.Models.Dog", b =>
                {
                    b.HasBaseType("Domain.Models.Animal.AnimalModel");

                    b.Property<string>("DogBreed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DogWeight")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Dog");
                });

            modelBuilder.Entity("Domain.Models.AnimalUser.AnimalUserModel", b =>
                {
                    b.HasOne("Domain.Models.Animal.AnimalModel", "Animal")
                        .WithMany("AnimalUsers")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User.UserModel", "User")
                        .WithMany("AnimalUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Animal.AnimalModel", b =>
                {
                    b.Navigation("AnimalUsers");
                });

            modelBuilder.Entity("Domain.Models.User.UserModel", b =>
                {
                    b.Navigation("AnimalUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
