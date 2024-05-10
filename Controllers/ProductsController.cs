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

    // GET: /products
    [AllowAnonymous]
    [HttpGet]
    [Route("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ProducesDefaultResponseType]
    public Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    { return _services.GetProducts(); }

    // GET: /products/{id}
    [AllowAnonymous]
    [HttpGet]
    [Route("products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ProducesDefaultResponseType]
    public Task<ActionResult<ProductDto>> GetProduct(int id)
    { return _services.GetProduct(id); }

    // POST: /products
    [AllowAnonymous]
    [HttpPost]
    [Route("products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ProducesDefaultResponseType]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductDtoRequest product)
    {
        await _services.PostProduct(product);
        return Ok(new ProductDto(product));
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




