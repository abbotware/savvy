namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Security;

[Table(nameof(Employee))]
public class Employee : Entity
{
    public Employee()
    {
        EntityType = EntityType.Employee;
    }

    [StringLength(100)]
    [Display(Order = 2)]
    public string? LastName { get; set; } = null!;

    [StringLength(100)]
    [Display(Order = 3)]
    public string? Email { get; set; } = null!;

    [StringLength(25)]
    [Display(Order = 4)]
    public string? Phone { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public Principal Principal { get; set; } = null!;
}