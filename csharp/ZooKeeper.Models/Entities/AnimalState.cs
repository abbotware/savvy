namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(AnimalState), Schema = Constants.EntitySchema)]
public class AnimalState : InsertableRecord
{
    public AnimalStatus Status { get; set; } = AnimalStatus.Unknown;

    public DateTimeOffset Effective { get; set; }

    public bool WasFed { get; set; }

    public string? Comments { get; set; }
}
