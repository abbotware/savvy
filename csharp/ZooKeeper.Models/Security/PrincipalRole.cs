namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(PrincipalRole), Schema = Constants.SecuritySchema)]
public class PrincipalRole : NamedEntity
{
    public Principal Principal { get; set; } = null!;

    public Role Role { get; set; } = null!;
}