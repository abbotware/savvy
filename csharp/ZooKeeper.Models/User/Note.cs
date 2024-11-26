namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Note))]
public class Note : UserEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ICollection<NoteUserEntity> Notes { get; } = new List<NoteUserEntity>();
}