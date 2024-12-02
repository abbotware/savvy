namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table(nameof(AnimalState), Schema = Constants.EntitySchema)]
public class AnimalState : InsertableRecord
{
    public AnimalState()
    {
        Effective = Created;
    }

    public AnimalStatus Status { get; set; } = AnimalStatus.Unknown;

    public DateTimeOffset Effective { get; set; }

    public bool WasFed { get; set; }

    public string? Comments { get; set; }

    [ForeignKey(nameof(Animal))]
    [Display(AutoGenerateField = false)]
    public long AnimalId { get; set; }

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public Animal Animal { get; set; } = null!;
}
