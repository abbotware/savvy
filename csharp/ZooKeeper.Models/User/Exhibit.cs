namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Metadata;

[Table(nameof(Exhibit), Schema = Constants.DataSchema)]
public class Exhibit : UserEntity
{
    public Exhibit()
    {
        EntityType = UserEntityType.Exhibit;
    }
    public Habitat? Habitat { get; set; }

    [ForeignKey(nameof(Habitat))]
    public long? HabitatId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<Animal> Animals { get; } = [];
}