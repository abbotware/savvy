namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models.Entities;

[Table(nameof(Principal), Schema = Constants.SecuritySchema)]
[Index(nameof(Name), IsUnique = true)]
public class Principal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public virtual string Name { get; set; } = string.Empty;

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public Employee? Employee { get; set; }

    [ForeignKey(nameof(Employee))]
    public long? EmployeeId { get; set; }

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Entity> CreatedEntities { get; set; } = [];

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Entity> UpdatedEntities { get; set; } = [];

    //[ForeignKey(nameof(PrincipalRoles))]
    //[Display(AutoGenerateField = false)]
    //[JsonIgnore]
    //public ICollection<Role> Roles { get; } = [];

    //[Display(AutoGenerateField = false)]
    //[JsonIgnore]
    //public ICollection<PrincipalRole> PrincipalRoles { get; } = [];

    //[Display(AutoGenerateField = false)]
    //[JsonIgnore]
    //[InverseProperty(nameof(PrincipalRole.CreatedBy))]
    //public ICollection<PrincipalRole> AssignedRoles { get; } = [];
}