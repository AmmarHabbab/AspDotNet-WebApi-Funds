using System.ComponentModel.DataAnnotations;

namespace WebApi1.Models;

public class PointOfInterestForCreationDto
{
    [Required(ErrorMessage="You Should provide a new value.")]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    [MaxLength(200)]
    public string? Description {get;set;}

    
}
