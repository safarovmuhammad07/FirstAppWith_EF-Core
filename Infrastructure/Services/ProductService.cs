using System.Net;
using Domain.Entities;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Intrefaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(Context context):IProductService
{
    public async Task<Responce<List<Product>>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var res = await context.Products.ToListAsync();
        return new Responce<List<Product>>(res);
    }

    public async Task<Responce<Product>> GetProductByIdAsync(int id)
    {
        var group = await context.Products.FirstOrDefaultAsync(g => g.Id == id);
        return group == null
            ? new Responce<Product>(HttpStatusCode.NotFound, "Group not found")
            : new Responce<Product>(group);
    }

    public async Task<Responce<string>> AddProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
        var result =await context.SaveChangesAsync();
        return result==0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Product added");
        
    }

    public async Task<Responce<string>> UpdateProductAsync(Product product)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(g => g.Id == product.Id);
        if (existingProduct == null)
        {
            return await Task.FromResult(new Responce<string>(HttpStatusCode.NotFound, "Product not found"));
        }
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        
        var result = await context.SaveChangesAsync();
        return result==0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Product updated");
    }

    public async Task<Responce<string>> DeleteProductAsync(int id)
    {
        var existindProduct = await context.Products.FirstOrDefaultAsync(g => g.Id == id);
        if (existindProduct == null)
        {
            return await Task.FromResult(new Responce<string>(HttpStatusCode.NotFound, "Product not found"));
            
        }
        context.Products.Remove(existindProduct);
        var result =await context.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Product deleted");
    }
}