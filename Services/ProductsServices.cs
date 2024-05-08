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
            return products;
        }
        catch (System.ArgumentNullException) { return new NotFoundResult(); }
    }

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }
        catch (System.Exception) { return new NotFoundResult(); }
    }
}
