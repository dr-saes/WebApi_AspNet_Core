using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_AspNet_Core;

public class ProductsServices : IProductsServices

{
    private readonly ApiDbContext? _context;

    public ProductsServices(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
            { return new NotFoundObjectResult($"The products were not found."); }
            return products;
        }
        catch (System.InvalidOperationException ex)
        { return new ObjectResult(ex.Message) { StatusCode = 500 }; }
        catch (System.Exception ex)
        { return new ObjectResult(ex.Message) { StatusCode = 500 }; }
    }

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            { return new NotFoundObjectResult($"The product with ID {id} was not found."); }
            return product;
        }
        catch (System.Exception ex)
        { return new ObjectResult(ex.Message) { StatusCode = 404 }; }
    }
}
