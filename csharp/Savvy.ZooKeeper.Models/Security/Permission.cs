﻿namespace Savvy.ZooKeeper.Models.Security;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Savvy.ZooKeeper.Models;

[Table(nameof(Permission), Schema = Constants.SecuritySchema)]
public class Permission : NamedRecord
{
    [ForeignKey(nameof(RolePermissions))]
    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<Role> Roles { get; } = [];

    [Display(AutoGenerateField = false)]
    [JsonIgnore]
    public ICollection<RolePermission> RolePermissions { get; } = [];
}