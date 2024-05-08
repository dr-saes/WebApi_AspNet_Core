using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public interface IProductsServices
{
    Task<ActionResult<IEnumerable<Product>>> GetProducts();
    Task<ActionResult<Product>> GetProduct(int id);
}
