using InstaPet.DomainModel.Entities;
using InstaPet.DomainModel.ValueObjects;
using InstaPet.Infrastructure.DataAccess.Contexts.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaPet.Infrastructure.DataAccess.Contexts
{
    public class InstaPetDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DbSpecie> Species { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(Properties.Resources.
                ResourceManager.GetString("DbConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
             .Entity<Pet>()
             .Property(pet => pet.Breed)
             .HasConversion(
                 breed => breed.ToString(),
                 breed => Breed.Parse(breed))
             .HasColumnName("Breed");

            modelBuilder
                .Entity<DbSpecie>()
                .Property(dbSpecie => dbSpecie.Specie)
                .HasConversion(
                    specie => specie.Name.ToString(),
                    specie => new Specie(specie))
                .HasColumnName("Specie");
        }
    }
}
