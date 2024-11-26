namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(RolePermission), Schema = Constants.SecuritySchema)]
public class RolePermission : NamedEntity
{
    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;

    public UserEntity? Target { get; }

    public bool Inheritable { get; set; }

    public bool Grantable { get; set; }

    public bool Deny { get; set; }
}