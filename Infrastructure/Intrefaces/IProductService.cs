using Domain.Entities;
using Infrastructure.ApiResponce;

namespace Infrastructure.Intrefaces;

public interface IProductService
{
    Task<Responce<List<Product>>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<Responce<Product>> GetProductByIdAsync(int id);
    Task<Responce<string>> AddProductAsync(Product product);
    Task<Responce<string>> UpdateProductAsync(Product product);
    Task<Responce<string>> DeleteProductAsync(int id);
}