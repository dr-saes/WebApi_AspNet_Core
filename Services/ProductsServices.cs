using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_AspNet_Core;

public class ProductsServices : ControllerBase, IProductsServices

{
    private readonly ApiDbContext? _context;

    public ProductsServices(ApiDbContext context)
    {
        _context = context;
    }

    public List<ProductDto> GetProducts()
    {
        var products = _context.Products.ToList();
        if (products == null || products.Count == 0)
        { return new List<ProductDto>(); }

        var productsDto = products.Select(p => new ProductDto
        {
            Price = p.Price,
            Name = p.Name,
            Description = p.Description
        }).ToList();

        return productsDto;
    }

    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            { return new NotFoundObjectResult($"The product with ID {id} was not found."); }

            var productDto = new ProductDto(product);

            return productDto;
        }
        catch (System.InvalidOperationException ex)
        { return new ObjectResult(ex.Message) { StatusCode = 500 }; }
        catch (System.Exception ex)
        { return new ObjectResult(ex.Message) { StatusCode = 404 }; }
    }

    public async Task<ActionResult<ProductDto>> PostProduct(ProductDtoRequest productDtoRequest)
    {
        if (productDtoRequest != null)
        {
            var product = new Product(productDtoRequest);


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                var productDto = new ProductDto(product);
                return productDto;
            }
            catch (DbUpdateException ex)
            { return new ObjectResult($"An error occurred while saving the product to the database: {ex.Message}") { StatusCode = 500 }; }
            catch (System.OperationCanceledException ex)
            { return new ObjectResult($"An error occurred while saving the product to the database: {ex.Message}") { StatusCode = 500 }; }
            catch (System.Exception ex)
            { return new ObjectResult($"An error occurred while creating the product: {ex.Message}") { StatusCode = 500 }; }
        }
        else
        { { return new ObjectResult("Product is null.") { StatusCode = 400 }; } }
    }

}
