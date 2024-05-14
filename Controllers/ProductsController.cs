using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Net;


namespace WebApi_AspNet_Core;

[Authorize]
[ApiController]
[Route("webapi-aspnet-core/")]
public class ProductsController : BasicController
{
    private readonly ApiDbContext _context;
    private readonly IProductsServices _services;

    public ProductsController(ApiDbContext context, IProductsServices services)
    {
        _context = context;
        _services = services;
    }


    // GET: /products
    /// <remarks>
    /// This route is used to search for all products registered in the database.
    /// </remarks>
    /// <summary>
    /// Check all products
    /// </summary>
    /// <response code="200">Returns all products.</response>
    /// <response code="500">Returns the error message indicating an unforeseen failure in the services. Contact the service provider.</response>
    [AllowAnonymous]
    [HttpGet]
    [Route("/products")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProducts()
    {
        try
        {
            List<ProductDto> productDtos = _services.GetProducts();
            return CreateResponse(HttpStatusCode.OK, productDtos);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (Exception ex)
        { return StatusCode(500, ex.Message); }
    }

    // GET: /products/{id}
    [AllowAnonymous]
    [HttpGet]
    [Route("products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProduct(int id)
    {
        int statusCode = 0;
        try
        {
            ProductDto productDto = _services.GetProduct(id);
            return CreateResponse(HttpStatusCode.OK, productDto);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (Exception ex)
        {
            if (ex.Message.Contains("404"))
            { statusCode = 404; }
            else { statusCode = 500; }
            return StatusCode(statusCode, ex.Message);
        }
    }

    // POST: /products
    [AllowAnonymous]
    [HttpPost]
    [Route("/products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult PostProduct(ProductDtoRequest product)
    {
        try
        {
            ProductDto productDto = _services.PostProduct(product);
            return CreateResponse(HttpStatusCode.Created, productDto);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (DbUpdateException ex)
        { return StatusCode(500, ex.Message); }
        catch (OperationCanceledException ex)
        { return StatusCode(500, ex.Message); }
    }


    // PUT: api/Products/{id}
    [AllowAnonymous]
    [HttpPut]
    [Route("/products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult PutProduct(int id, ProductDto productRequest)
    {
        int statusCode = 0;
        try
        {
            ProductDto productDto = _services.PutProduct(id, productRequest);
            return CreateResponse(HttpStatusCode.NoContent);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (DbUpdateException ex)
        { return StatusCode(500, ex.Message); }
        catch (OperationCanceledException ex)
        { return StatusCode(500, ex.Message); }
        catch (Exception ex)
        {
            if (ex.Message.Contains("404"))
            { statusCode = 404; }
            else { statusCode = 500; }
            return StatusCode(statusCode, ex.Message);
        }
    }

    // DELETE: api/Products/{id}
    //Authorize(Roles = "Admin")]
    [AllowAnonymous]
    [HttpDelete]
    [Route("products/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteProduct(int id)
    {
        int statusCode = 0;
        try
        {
            ProductDto productDto = _services.DeleteProduct(id);
            return CreateResponse(HttpStatusCode.NoContent);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (DbUpdateException ex)
        { return StatusCode(500, ex.Message); }
        catch (OperationCanceledException ex)
        { return StatusCode(500, ex.Message); }
        catch (Exception ex)
        {
            if (ex.Message.Contains("404"))
            { statusCode = 404; }
            else { statusCode = 500; }
            return StatusCode(statusCode, ex.Message);
        }
    }

}




