namespace Savvy.ZooKeeper.Models;

using System.ComponentModel.DataAnnotations;

public abstract class NamedRecord : InsertableRecord
{
    [StringLength(100)]
    [Required(ErrorMessage = "Please provide a name.")]
    [Display(Order = 1, Description = "Name", ShortName = "Name", Prompt = "Please Provide a Name")]
    public string Name { get; set; } = null!;
}
