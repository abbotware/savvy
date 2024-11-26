namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;

public abstract class NamedEntity : InsertableEntity
{

    [StringLength(100)]
    [Required(ErrorMessage = "Please provide a name.")]
    [Display(Order = 100, Description = "Name", ShortName = "Name", Prompt = "Please Provide a Name")]
    public string Name { get; set; } = null!;
}
