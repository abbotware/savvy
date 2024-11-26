namespace Savvy.ZooKeeper.Models.Metadata;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Savvy.ZooKeeper.Models;

[Table(nameof(Habitat), Schema = Constants.MetadataSchema)]
public class Habitat : UpdatableEntity
{
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<AnimalType> AnimalTypes { get; } = [];
}