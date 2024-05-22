using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_AspNet_Core;

public class Product
{
    public Product()
    { }
    public Product(ProductDtoRequest productDtoRequest)
    {
        Name = productDtoRequest.Name;
        Description = productDtoRequest.Description;
        Price = productDtoRequest.Price;
        StockQuantity = productDtoRequest.StockQuantity;

        SuppliersTypeEnum? supplierTypeEnum = Enum.TryParse<SuppliersTypeEnum>(productDtoRequest.SupplierType.ToString(), out var parsedEnum)
               ? parsedEnum
               : null;
        SupplierType = parsedEnum;


    }


    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; }
    public SuppliersTypeEnum SupplierType { get; set; }




}
