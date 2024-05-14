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

    //GetAll
    public List<ProductDto> GetProducts()
    {
        List<ProductDto> ProductsDto = new List<ProductDto>();
        var products = _context.Products.ToList();
        if (products == null || products.Count == 0)
        { return new List<ProductDto>(); }

        foreach (var product in products)
        {
            var productDto = new ProductDto(product);
            ProductsDto.Add(productDto);
        }

        return ProductsDto;
    }

    //GetId
    public ProductDto GetProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
            throw new Exception($"The product with ID {id} was not found. (404)");
        var productDto = new ProductDto(product);
        return productDto;
    }

    //Post
    public ProductDto PostProduct(ProductDtoRequest productDtoRequest)
    {
        if (!ModelState.IsValid)
            throw new Exception($"Bad Rquest - The product is invalid. (400)");
        var product = new Product(productDtoRequest);
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            var productDto = new ProductDto(product);
            return productDto;
        }
    }

    //PutId
    public ProductDto PutProduct(int id, ProductDto productDto)
    {

        var product = _context.Products.Find(id);
        if (product == null)
            throw new Exception($"The product with ID {id} was not found. (404)");
        else
        {
            product.Name = string.IsNullOrEmpty(productDto.Name) ? product.Name : productDto.Name;
            product.Description = string.IsNullOrEmpty(productDto.Description) ? product.Description : productDto.Description;
            product.Price = productDto.Price == 0 ? product.Price : productDto.Price;
            product.StockQuantity = productDto.StockQuantity == 0 ? product.StockQuantity : productDto.StockQuantity;
            _context.Products.Update(product);
            _context.SaveChanges();
            var productDtoNew = new ProductDto(product);
            return productDtoNew;
        }
    }

    public ProductDto DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
            throw new Exception($"The product with ID {id} was not found. (404)");
        _context.Products.Remove(product);
        _context.SaveChanges();
        var productDto = new ProductDto(product);
        return productDto;
    }
}
