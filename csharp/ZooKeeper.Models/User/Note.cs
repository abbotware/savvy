namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;

public class Note : UserEntity
{
    public long EntityId { get; set; }

    [Required]
    public UserEntity Entity { get; set; } = null!;
}