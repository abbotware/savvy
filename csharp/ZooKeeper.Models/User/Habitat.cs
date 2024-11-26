namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table(nameof(Habitat), Schema = Constants.DataSchema)]
public class Habitat : UserEntity
{
    public Habitat()
    {
        EntityType = UserEntityType.Habitat;
    }

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Animal> Animals { get; } = [];
}