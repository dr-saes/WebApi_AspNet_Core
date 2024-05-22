using System.ComponentModel.DataAnnotations;
using static WebApi_AspNet_Core.ValidationState;

namespace WebApi_AspNet_Core;

public class ProductDtoRequest
{

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [Range(1, int.MaxValue, ErrorMessage = "The price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [Range(1, int.MaxValue, ErrorMessage = "The price must be greater than zero.")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [AllowedSupplierType(ErrorMessage = "SupplierType must be one of 0, 1, 2, or 3.")]
    public int SupplierType { get; set; }






}
