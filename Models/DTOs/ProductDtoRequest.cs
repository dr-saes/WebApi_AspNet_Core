using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class ProductDtoRequest
{
    public ProductDtoRequest() { }

    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int StockQuantity { get; set; }

}
