namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Savvy.ZooKeeper.Models.Entities;

[Table(nameof(RolePermission), Schema = Constants.SecuritySchema)]
public class RolePermission : NamedRecord
{
    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;

    public Entity? Target { get; }

    public bool Inheritable { get; set; }

    public bool Grantable { get; set; }

    public bool Deny { get; set; }
}