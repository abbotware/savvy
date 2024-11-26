namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public abstract class UserEntity : SystemEntity
{
    public virtual UserEntityType EntityType { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<NoteUserEntity> Notes { get; } = new List<NoteUserEntity>();
}
