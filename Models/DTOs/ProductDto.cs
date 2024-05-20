using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class ProductDto
{
    public ProductDto() { }

    public ProductDto(Product product)
    {
        this.Name = product.Name;
        this.Price = product.Price;
        this.Description = product.Description;
        this.StockQuantity = product.StockQuantity;

    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }








}
