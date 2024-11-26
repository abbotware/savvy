namespace Savvy.ZooKeeper.Models;

using System;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Metadata;

public class ModelContext(DbContextOptions<ModelContext> options) 
    : DbContext(options)
{
    public DbSet<AnimalType> AnimalTypes => Set<AnimalType>();
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Habitat> Habitats => Set<Habitat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .UseTptMappingStrategy();

        //modelBuilder.Entity<UserEntity>()
        //    .HasDiscriminator<UserEntityType>("EntityType")
        //    .HasValue<Animal>(UserEntityType.Animal)
        //    .HasValue<Employee>(UserEntityType.Employee)
        //    .HasValue<Note>(UserEntityType.Note)
        //    .HasValue<Habitat>(UserEntityType.Habitat);

        //modelBuilder.Entity<UserEntity>()
        //  .HasMany(e => e.Notes)
        //  .WithMany(e => e.Entities)
        //  .UsingEntity<NoteUserEntity>();

        base.OnModelCreating(modelBuilder);
    }
}
