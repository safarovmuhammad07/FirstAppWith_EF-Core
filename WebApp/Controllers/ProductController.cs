using Domain.Entities;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Intrefaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService):ControllerBase
{
    [HttpGet] public async Task<Responce<List<Product>>> GetProducts()=>await productService.GetProductsAsync();
    [HttpGet("{id}")] public async Task<Responce<Product>> GetProductById(int id)=>await productService.GetProductByIdAsync(id);
    [HttpPost] public async Task<Responce<string>> AddProduct(Product product)=>await productService.AddProductAsync(product);
    [HttpPut] public async Task<Responce<string>> UpdateProduct(Product product)=>await productService.UpdateProductAsync(product);
    [HttpDelete] public async Task<Responce<string>> DeleteProduct(int id)=>await productService.DeleteProductAsync(id);
}