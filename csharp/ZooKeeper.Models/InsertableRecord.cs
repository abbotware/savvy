namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Security;

public abstract class InsertableRecord : IIdentifiable<long>
{
    [Key]
    [ReadOnly(true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    [Display(AutoGenerateField = false)]
    public virtual Principal CreatedBy { get; set; } = null!;
}
