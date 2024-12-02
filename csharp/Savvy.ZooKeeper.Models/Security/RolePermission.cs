namespace Savvy.ZooKeeper.Models.Security;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Entities;

[Table(nameof(RolePermission), Schema = Constants.SecuritySchema)]
public class RolePermission : InsertableRecord
{
    [ForeignKey(nameof(Permission))]
    public long PermissionId { get; set; }

    [ForeignKey(nameof(Role))]
    public long RoleId { get; set; }

    [ForeignKey(nameof(Entity))]
    public long? EntityId { get; set; }

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Role Role { get; set; } = null!;

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Permission Permission { get; set; } = null!;

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Entity? Target { get; }

    public bool Inheritable { get; set; }

    public bool Grantable { get; set; }

    public bool Deny { get; set; }
}