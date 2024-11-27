namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Table("Entity", Schema = Constants.EntitySchema)]
[Index(nameof(Name), nameof(EntityType), IsUnique = true)]
public abstract class Entity : UpdatableRecord
{
    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public virtual EntityType EntityType { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<NoteEntity> HasNotes { get; } = [];
}
