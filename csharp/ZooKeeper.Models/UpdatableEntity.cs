namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

public abstract class UpdatableEntity : NamedEntity
{
    public UpdatableEntity()
    {
        Updated = Created;
    }

    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public DateTimeOffset Updated { get; set; }

    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    [ForeignKey(nameof(UpdatedBy))]
    public long UpdatedById { get; set; }

    [ReadOnly(true)]
    [JsonIgnore]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Principal UpdatedBy { get; set; } = null!;

    [Timestamp]
    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public byte[] Version { get; set; } = null!;
}
