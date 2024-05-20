using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_AspNet_Core;

public class Product
{
    private ProductDtoRequest productDtoRequest;
    private ProductDto productDto;

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
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; }

    [ForeignKey("SupplierId")]
    public virtual Supplier Supplier { get; set; }

}
