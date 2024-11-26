namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(AnimalState), Schema = Constants.DataSchema)]
public class AnimalState : InsertableEntity
{
    public AnimalStatus Status { get; set; } = AnimalStatus.Unknown;

    public DateTimeOffset Effective { get; set; }
}
