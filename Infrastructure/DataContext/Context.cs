using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext;
public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}