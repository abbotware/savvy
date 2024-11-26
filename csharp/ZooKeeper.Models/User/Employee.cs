namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Employee))]
public class Employee : UserEntity
{
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)] 
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(25)]
    public string Phone { get; set; } = null!;
}