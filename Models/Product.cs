using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class Product
{
    private ProductDtoRequest productDtoRequest;

    public Product()
    {

    }
    public Product(ProductDtoRequest productDtoRequest)
    {
        Name = productDtoRequest.Name;
        Description = productDtoRequest.Description;
        Price = productDtoRequest.Price;
        StockQuantity = productDtoRequest.StockQuantity;
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    [Range(1, int.MaxValue, ErrorMessage = "The price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string? Description { get; set; }
}
