namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public abstract class UserEntity : SystemEntity
{
    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public virtual UserEntityType EntityType { get; set; }

    [StringLength(1000)]
    [Display(Order = 50, Description = "Description", ShortName = "Description")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Timestamp]
    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public byte[] Version { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [Display(AutoGenerateField = false)]
    public ICollection<NoteUserEntity> HasNotes { get; } = [];
}
