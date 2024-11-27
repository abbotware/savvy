namespace Savvy.ZooKeeper.Models;

using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Entities;
using Savvy.ZooKeeper.Models.Metadata;

public class ModelContext(DbContextOptions<ModelContext> options) 
    : DbContext(options)
{
    public DbSet<AnimalType> AnimalTypes => Set<AnimalType>();

    public DbSet<Habitat> HabitatTypes => Set<Habitat>();

    public DbSet<Animal> Animals => Set<Animal>();

    public DbSet<Exhibit> Exhibits => Set<Exhibit>();

    public DbSet<Note> Notes => Set<Note>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Principal> Principals => Set<Principal>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Permission> Permissions => Set<Permission>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>()
            .UseTptMappingStrategy();

        modelBuilder.Entity<Principal>()
            .HasOne(e => e.Employee)
            .WithOne(e => e.Principal)
            .HasForeignKey<Employee>(e => e.PrincipalId)
            .IsRequired(false);

        modelBuilder.Entity<Entity>()
            .HasOne(e => e.CreatedBy)
            .WithMany(x => x.CreatedEntities)
            .HasForeignKey(x => x.CreatedById);

        modelBuilder.Entity<Entity>()
            .HasOne(e => e.UpdatedBy)
            .WithMany(x => x.UpdatedEntities)
            .HasForeignKey(x => x.UpdatedById);

        base.OnModelCreating(modelBuilder);
    }
}
