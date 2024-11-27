namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Metadata;

[Table(nameof(Exhibit), Schema = Constants.EntitySchema)]
public class Exhibit : Entity
{
    public Exhibit()
    {
        EntityType = EntityType.Exhibit;
    }
    public Habitat? Habitat { get; set; }

    [ForeignKey(nameof(Habitat))]
    public long? HabitatId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Animal> Animals { get; } = [];
}