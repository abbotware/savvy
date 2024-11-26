namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Habitat))]
public class Habitat : UserEntity
{
    public ICollection<Animal> Animals { get; } = new List<Animal>();
}