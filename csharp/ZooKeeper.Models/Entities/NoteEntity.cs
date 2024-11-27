namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(NoteId), nameof(UserEntityId))]
[Table("NoteEntity", Schema = Constants.EntitySchema)]
public class NoteEntity
{
    [ForeignKey(nameof(Note))]
    public long NoteId { get; set; }

    [ForeignKey(nameof(Entity))]
    public long UserEntityId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Note Note { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Entity Entity { get; set; } = null!;
}
