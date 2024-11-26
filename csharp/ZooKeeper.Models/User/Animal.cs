namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Animal))]
public class Animal : UserEntity
{
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