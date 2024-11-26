﻿namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Principal), Schema = Constants.SecuritySchema)]
[Index(nameof(Name), IsUnique = true)]
public class Principal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public Employee? Employee { get; set; }

    [ForeignKey(nameof(Employee))]
    public long? EmployeeId { get; set; }

    public ICollection<UserEntity> CreatedEntities { get; set; } = [];

    public ICollection<UserEntity> UpdatedEntities { get; set; } = [];

}