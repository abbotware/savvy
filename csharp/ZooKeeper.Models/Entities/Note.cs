namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Note), Schema = Constants.EntitySchema)]
public class Note : Entity
{
    public Note()
    {
        EntityType = EntityType.Note;
    }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [JsonIgnore]
    public ICollection<NoteEntity> NoteOf { get; } = [];
}
