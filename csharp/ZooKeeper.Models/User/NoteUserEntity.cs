namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(NoteId), nameof(UserEntityId))]
public class NoteUserEntity
{
    [ForeignKey(nameof(Note))]
    public long NoteId { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public long UserEntityId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Note Note { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public UserEntity UserEntity { get; set; }

}
