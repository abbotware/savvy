namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
public class SystemEntity
{
    public SystemEntity()
    {
        Created = DateTimeOffset.Now;
        Updated = Created;
    }

    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    [Timestamp]
    public long Version { get; set; }
}
