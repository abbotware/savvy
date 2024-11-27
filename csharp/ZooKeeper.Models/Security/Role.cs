namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Role), Schema = Constants.SecuritySchema)]
public class Role : NamedRecord
{
}