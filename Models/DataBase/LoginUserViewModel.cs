using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class LoginUserViewModel
{
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    [EmailAddress(ErrorMessage = "The field {0} is in an invalid format.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "The field {0} is mandatory.")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 6)]
    public string? Password { get; set; }

}
