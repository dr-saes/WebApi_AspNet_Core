using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_AspNet_Core;

public class Supplier
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [StringLength(14, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
    public string? Document { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string? SupplierType { get; set; }
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string? PersonType { get; set; }
    public bool Active { get; set; }





}
