using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public interface IProductsServices
{
    List<ProductDto> GetProducts();
    ProductDto GetProduct(int id);
    ProductDto PostProduct(ProductDtoRequest productDtoRequest);
    ProductDto PutProduct(int id, ProductDtoRequestPut productDtoRequest);
    ProductDto DeleteProduct(int id);


}
