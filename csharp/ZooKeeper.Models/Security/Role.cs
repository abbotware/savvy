namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
}