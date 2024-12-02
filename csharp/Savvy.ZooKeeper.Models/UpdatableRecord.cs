namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Security;

public abstract class UpdatableRecord : NamedRecord
{
    public UpdatableRecord()
    {
        Updated = Created;
    }

    [StringLength(1000)]
    [Display(Order = 50, Description = "Description", ShortName = "Description", Prompt = "Description (optional)")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

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
    [Display(AutoGenerateField = false)]
    public Principal UpdatedBy { get; set; } = null!;

    [Timestamp]
    [ConcurrencyCheck]
    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public byte[] Version { get; set; } = Array.Empty<byte>();
}
