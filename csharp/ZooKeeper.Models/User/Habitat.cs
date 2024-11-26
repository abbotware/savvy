namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Habitat), Schema = Constants.UserEntitySchema)]
public class Habitat : UserEntity
{
    public Habitat()
    {
        EntityType = UserEntityType.Habitat;
    }

    [Display(AutoGenerateField = false)]
    public ICollection<Animal> Animals { get; } = [];
}