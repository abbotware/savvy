namespace Savvy.ZooKeeper.Models.Metadata;

using System.ComponentModel.DataAnnotations.Schema;
using Savvy.ZooKeeper.Models;

[Table(nameof(AnimalType), Schema = "Metadata")]
public class AnimalType : SystemEntity
{
    //public HierarchyId Taxonomy { get; set; } = null!;
    public string Diet { get; set; } = null!;
    public string Habitat { get; set; } = null!;
}