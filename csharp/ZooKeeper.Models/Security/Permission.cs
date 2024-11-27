namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Permission), Schema = Constants.SecuritySchema)]
public class Permission : NamedRecord
{
}