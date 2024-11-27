namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Savvy.ZooKeeper.Models.Metadata;

[Table(nameof(Animal), Schema = Constants.EntitySchema)]
public class Animal : Entity
{
    public Animal()
    {
        EntityType = EntityType.Animal;
    }

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public AnimalType AnimalType { get; set; } = null!;

    [ForeignKey(nameof(AnimalType))]
    public long AnimalTypeId { get; set; }

    public DateTimeOffset EnteredCaptivitiy { get; set; }

    public DateTimeOffset Birth { get; set; }

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public DateOnly BirthDate => DateOnly.FromDateTime(Birth.LocalDateTime);

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Exhibit? CurrentExhibit { get; set; }

    [ForeignKey(nameof(Exhibit))]
    public long? ExhibitId { get; set; }

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public AnimalState? CurrentState { get; set; }

    [ForeignKey(nameof(AnimalState))]
    public long? AnimalStateId { get; set; }

    [DataType(DataType.MultilineText)]
    [JsonIgnore]
    public string? Diet { get; set; } = null!;

    [DataType(DataType.MultilineText)]
    [JsonIgnore]
    public string? FeedingTimes { get; set; } = null!;

    [JsonPropertyName("Diet")]
    public string? ActualDiet => Diet ?? AnimalType?.Diet;

    [JsonPropertyName("FeedingTimes")]
    public string? ActualFeedingTimes => FeedingTimes ?? AnimalType?.FeedingTimes;
}