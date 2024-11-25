namespace Savvy.ZooKeeper.Models;

public class UserEntity : SystemEntity
{
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}
