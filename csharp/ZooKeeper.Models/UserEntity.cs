namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Table("Entity", Schema = Constants.DataSchema)]
[Index(nameof(Name),nameof(EntityType), IsUnique = true)]
public abstract class UserEntity : UpdatableEntity
{
    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public virtual UserEntityType EntityType { get; set; }

    [StringLength(1000)]
    [Display(Order = 50, Description = "Description", ShortName = "Description", Prompt = "Description (optional)")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<NoteUserEntity> HasNotes { get; } = [];
}
