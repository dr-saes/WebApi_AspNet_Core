
using System.ComponentModel.DataAnnotations;
using static WebApi_AspNet_Core.Validations;


namespace WebApi_AspNet_Core;

public class ProductDtoRequestPut
{


    public string Name { get; set; }


    public string Description { get; set; }

    [PriceValidator(ErrorMessage = "The price must be greater than zero.")]
    public decimal Price { get; set; }


    [Range(1, int.MaxValue, ErrorMessage = "The quantity must be greater than zero.")]
    public int StockQuantity { get; set; }


    [AllowedType(ErrorMessage = "SupplierType must be one of 0, 1, 2, or 3.")]
    public int SupplierType { get; set; }






}
