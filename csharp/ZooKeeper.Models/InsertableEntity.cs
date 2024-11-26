namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

public abstract class InsertableEntity
{
    [Key]
    [ReadOnly(true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(AutoGenerateField = false)]
    public long Id { get; set; }

    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;

    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    [ForeignKey(nameof(CreatedBy))]
    public long CreatedById { get; set; }

    [JsonIgnore]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Principal CreatedBy { get; set; } = null!;
}
