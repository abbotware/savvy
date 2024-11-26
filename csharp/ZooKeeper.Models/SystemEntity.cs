namespace Savvy.ZooKeeper.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class SystemEntity
{
    public SystemEntity()
    {
        Created = DateTimeOffset.Now;
        Updated = Created;
    }

    [Key]
    [ReadOnly(true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(AutoGenerateField = false)]
    public long Id { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Please provide a name.")]
    [Display(Order = 100, Description = "Name", ShortName = "Name", Prompt = "Please Provide a Name")]
    public string Name { get; set; } = null!;

    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public DateTimeOffset Created { get; set; }

    [ReadOnly(true)]
    [Display(AutoGenerateField = false)]
    public DateTimeOffset Updated { get; set; }
}
