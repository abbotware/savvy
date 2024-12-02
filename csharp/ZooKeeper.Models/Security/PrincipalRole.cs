namespace Savvy.ZooKeeper.Models.Security;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Savvy.ZooKeeper.Models;

[Table(nameof(PrincipalRole), Schema = Constants.SecuritySchema)]
public class PrincipalRole : InsertableRecord
{
    [ForeignKey(nameof(Principal))]
    public long PrincipalId { get; set; }

    [ForeignKey(nameof(Role))]
    public long RoleId { get; set; }

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Principal Principal { get; set; } = null!;

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Role Role { get; set; } = null!;
}