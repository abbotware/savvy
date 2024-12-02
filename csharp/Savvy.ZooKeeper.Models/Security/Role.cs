namespace Savvy.ZooKeeper.Models.Security;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Savvy.ZooKeeper.Models;

[Table(nameof(Role), Schema = Constants.SecuritySchema)]
public class Role : NamedRecord
{
    [ForeignKey(nameof(PrincipalRoles))]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Principal> Principals { get; } = [];

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<PrincipalRole> PrincipalRoles { get; } = [];

    [ForeignKey(nameof(RolePermissions))]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Permission> Permissions { get; } = [];

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<RolePermission> RolePermissions { get; } = [];
}