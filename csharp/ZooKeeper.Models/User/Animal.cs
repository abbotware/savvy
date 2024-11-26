namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Savvy.ZooKeeper.Models.Metadata;


[Table(nameof(Animal), Schema = Constants.DataSchema)]
public class Animal : UserEntity
{
    public Animal()
    {
        EntityType = UserEntityType.Animal;
    }

    public AnimalType AnimalType { get; set; } = null!;

    [ForeignKey(nameof(AnimalType))]
    public long AnimalTypeId { get; set; }

    public DateTimeOffset EnteredCaptivitiy { get; set; }

    public Habitat? CurrentHabitat { get; set; }

    [ForeignKey(nameof(Habitat))]
    public long? CurrentHabitatId { get; set; }

    public AnimalState? CurrentState { get; set; }

    [ForeignKey(nameof(AnimalState))]
    public long? AnimalStateId { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Diet { get; set; } = null!;
}