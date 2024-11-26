namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;

public abstract class SystemEntity
{
    public SystemEntity()
    {
        Created = DateTimeOffset.Now;
        Updated = Created;
    }

    [Key]
    public long Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }
}
