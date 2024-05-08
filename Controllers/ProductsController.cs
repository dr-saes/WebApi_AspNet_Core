using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace WebApi_AspNet_Core;

[Authorize]
[ApiController]
[Route("webapi-aspnet-core/")]
public class ProductsController : ControllerBase, IProductsServices
{
    private readonly ApiDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IProductsServices _services;

    public ProductsController(ApiDbContext context, IConfiguration configuration, IProductsServices services)
    {
        _context = context;
        _configuration = configuration;
        _services = services;
    }

    // GET: api/Products
    [AllowAnonymous]
    [HttpGet]
    [Route("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public Task<ActionResult<IEnumerable<Product>>> GetProducts()
    { return _services.GetProducts(); }

    // GET: api/Products/{id}
    //[AllowAnonymous]
    [HttpGet]
    [Route("products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public Task<ActionResult<Product>> GetProduct(int id)
    { return _services.GetProduct(id); }

    // POST: api/Products
    [HttpPost]
    [Route("products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {

        if (_context.Products == null) return Problem("Error creating the product, contact support.");

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Products/{id}
    [HttpPut]
    [Route("products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Product>> PutProduct(int id, Product productRequest)
    {
        var product = _context.Products.FirstOrDefault(item => item.Id == id);
        if (product == null) { return BadRequest("Product object is null"); }
        if (productRequest == null) { return BadRequest("Product object is null"); }
        if (id != product.Id) { return BadRequest("Parameter ID is different from product ID!"); }
        if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

        product.Name = productRequest.Name;
        product.Price = productRequest.Price;
        product.StockQuantity = productRequest.StockQuantity;
        product.Description = productRequest.Description;

        _context.Products.Update(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DBConcurrencyException ex)
        {
            if (!ProductExists(id)) { return NotFound(); }
            else { throw new Exception(ex.Message); }
        }

        return NoContent();
    }


    // DELETE: api/Products/{id}
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        if (_context.Products == null) return NotFound();
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}




