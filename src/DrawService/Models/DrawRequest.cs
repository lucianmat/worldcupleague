
namespace DrawService.Models;

/// <summary>
/// Request to execute a draw
/// </summary>
public class DrawRequest  : IValidatableObject
{
    [Required]
    [Description("User who execute draw")]
    [StringLength(50, MinimumLength = 3)]
    public required string UserName { get; set; }

    [Required]
    [Description("Number of groups, required to be  4 or 8")]
    public  int NumberOfGroups { get; set; } = 8;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    { 
        if (NumberOfGroups != 4 && NumberOfGroups != 8)
        {
            yield return new ValidationResult("Number of groups must be 4 or 8", new[] { nameof(NumberOfGroups) });
        }
    }
}