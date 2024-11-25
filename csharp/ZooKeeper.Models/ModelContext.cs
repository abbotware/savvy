namespace Savvy.ZooKeeper.Models;

using Microsoft.EntityFrameworkCore;

public class ModelContext : DbContext
{
    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public DbSet<AnimalType> Customers => Set<AnimalType>();
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Habitat> Habitats => Set<Habitat>();
}
