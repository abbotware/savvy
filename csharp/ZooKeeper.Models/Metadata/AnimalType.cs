namespace Savvy.ZooKeeper.Models.Metadata;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;

[Table(nameof(AnimalType), Schema = "Metadata")]
public class AnimalType : SystemEntity
{
    //public HierarchyId Taxonomy { get; set; } = null!;
    public string Diet { get; set; } = null!;
    
    [Required]
    [DeleteBehavior(DeleteBehavior.NoAction)]
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