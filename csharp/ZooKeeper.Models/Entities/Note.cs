namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Note), Schema = Constants.EntitySchema)]
public class Note : Entity
{
    public Note()
    {
        EntityType = EntityType.Note;
    }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<NoteEntity> NoteOf { get; } = [];
}
