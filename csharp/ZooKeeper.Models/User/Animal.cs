namespace Savvy.ZooKeeper.Models;

public class Animal : UserEntity
{
    public AnimalStatus Status { get; set; } = AnimalStatus.Unknown;

    public AnimalType Type { get; set; } = null!;

    public DateTimeOffset Captivitiy { get; set; }

    public Habitat CurrentHabitat { get; set; } = null!;

    public string? Diet { get; set; } = null!;
}