namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Note), Schema = Constants.DataSchema)]
public class Note : UserEntity
{
    public Note()
    {
        EntityType = UserEntityType.Note;
    }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<NoteUserEntity> NoteOf { get; } = [];
}