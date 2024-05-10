using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public interface IProductsServices
{
    Task<ActionResult<IEnumerable<ProductDto>>> GetProducts();
    Task<ActionResult<ProductDto>> GetProduct(int id);
    Task<ActionResult<ProductDto>> PostProduct(ProductDtoRequest product);

}
