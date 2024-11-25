namespace Savvy.ZooKeeper.Models;

using Microsoft.EntityFrameworkCore;

public class AnimalType : SystemEntity
{
    public HierarchyId Taxonomy { get; set; } = null!;
    public string Diet { get; set; } = null!;
    public string Habitat { get; set; } = null!;
}