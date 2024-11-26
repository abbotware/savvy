namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(AnimalType), Schema = "Metadata")]
public class AnimalType : SystemEntity
{
    //public HierarchyId Taxonomy { get; set; } = null!;
    public string Diet { get; set; } = null!;
    public string Habitat { get; set; } = null!;
}