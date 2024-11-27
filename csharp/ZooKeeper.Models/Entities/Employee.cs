namespace Savvy.ZooKeeper.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Employee))]
public class Employee : Entity
{
    public Employee()
    {
        EntityType = EntityType.Employee;
    }

    [StringLength(100)] 
    public string? LastName { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; } = null!;

    [StringLength(25)]
    public string? Phone { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [JsonIgnore]
    public Principal Principal { get; set; } = null!;
}