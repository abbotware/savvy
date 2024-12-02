namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(PrincipalRole), Schema = Constants.SecuritySchema)]
public class PrincipalRole : InsertableRecord
{
    [ForeignKey(nameof(Principal))]
    public long PrincipalId { get; set; }

    [ForeignKey(nameof(Role))]
    public long RoleId { get; set; }

    public Principal Principal { get; set; } = null!;

    public Role Role { get; set; } = null!;
}