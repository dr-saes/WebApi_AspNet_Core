using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public interface IProductsServices
{
    List<ProductDto> GetProducts();
    ProductDto GetProduct(int id);
    ProductDto PostProduct(ProductDtoRequest productDtoRequest);
    ProductDto PutProduct(int id, ProductDto productDto);
    ProductDto DeleteProduct(int id);


}
