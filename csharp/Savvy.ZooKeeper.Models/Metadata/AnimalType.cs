namespace Savvy.ZooKeeper.Models.Metadata;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;

[Table(nameof(AnimalType), Schema = Constants.MetadataSchema)]
[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Species), IsUnique = true)]
[Index(nameof(Kingdom), nameof(Phylum), nameof(Class), nameof(Order), nameof(Family), nameof(Genus), nameof(Species), IsUnique = true)]
public class AnimalType : UpdatableRecord
{
    [DataType(DataType.MultilineText)]
    public string Diet { get; set; } = null!;

    [DataType(DataType.MultilineText)]
    public string FeedingTimes { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Habitat Habitat { get; set; } = null!;

    [ForeignKey(nameof(Habitat))]
    public long HabitatId { get; set; }

    public string? Kingdom { get; set; }

    public string? Phylum { get; set; }

    public string? Class { get; set; }

    public string? Order { get; set; }

    public string? Family { get; set; }

    public string? Genus { get; set; }

    public string? Species { get; set; }
}