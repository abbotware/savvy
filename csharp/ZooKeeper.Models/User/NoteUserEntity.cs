namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(NoteId), nameof(UserEntityId))]
[Table("Note_Entity", Schema = Constants.DataSchema)]
public class NoteUserEntity
{
    [ForeignKey(nameof(Note))]
    public long NoteId { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public long UserEntityId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Note Note { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public UserEntity UserEntity { get; set; } = null!;
}
