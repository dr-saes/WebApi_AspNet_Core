using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class SupplierDtoRequest
{

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [StringLength(14, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
    public string? Document { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public int SupplierType { get; set; }
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public int PersonType { get; set; }
    public bool Active { get; set; }

}
