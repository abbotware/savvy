using Savvy.ZooKeeper.Models.Metadata;

namespace Savvy.ZooKeeper.Components.Pages.Metadata
{
    public partial class AnimalTypes
    {
        public string[] Initial = [
            nameof(AnimalType.Kingdom),
            nameof(AnimalType.Phylum),
            nameof(AnimalType.Class),
            nameof(AnimalType.Order),
            nameof(AnimalType.Family),
            nameof(AnimalType.Genus),
            nameof(AnimalType.Species)];
    }
}