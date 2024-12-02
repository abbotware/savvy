namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Metadata;

[Table(nameof(Exhibit), Schema = Constants.EntitySchema)]
public class Exhibit : Entity
{
    public Exhibit()
    {
        EntityType = EntityType.Exhibit;
    }
    
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Habitat Habitat { get; set; } = null!;

    [ForeignKey(nameof(Habitat))]
    public long HabitatId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Animal> Animals { get; } = [];
}