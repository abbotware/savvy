namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Savvy.ZooKeeper.Models.Metadata;

[Table(nameof(Animal), Schema = Constants.UserEntitySchema)]
public class Animal : UserEntity
{
    public Animal()
    {
        EntityType = UserEntityType.Animal;
    }

    public AnimalStatus Status { get; set; } = AnimalStatus.Unknown;

    public AnimalType AnimalType { get; set; } = null!;

    [ForeignKey(nameof(AnimalType))]
    public long AnimalTypeId { get; set; }

    public DateTimeOffset EnteredCaptivitiy { get; set; }

    public Habitat? CurrentHabitat { get; set; }

    [ForeignKey(nameof(Habitat))]
    public long? CurrentHabitatId { get; set; }

    public string? Diet { get; set; } = null!;
}