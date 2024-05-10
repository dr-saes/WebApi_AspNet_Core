using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class ProductDto
{
    public ProductDto() { }

    public ProductDto(Product product, int id)
    {
        this.Name = product.Name;
        this.Price = product.Price;
        this.Description = product.Description;
    }

    public ProductDto(List<Product> products)
    {
        foreach (var product in products)
        {
            this.Name = product.Name;
            this.Price = product.Price;
            this.Description = product.Description;
        }

    }

    public ProductDto(Product product)
    {
        this.Name = product.Name;
        this.Price = product.Price;
        this.Description = product.Description;
        this.StockQuantity = product.StockQuantity;
    }

    public ProductDto(ProductDtoRequest productDtoRequest)
    {
        this.Name = productDtoRequest.Name;
        this.Price = productDtoRequest.Price;
        this.Description = productDtoRequest.Description;
        this.StockQuantity = productDtoRequest.StockQuantity;
    }

    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Description { get; set; }

}
