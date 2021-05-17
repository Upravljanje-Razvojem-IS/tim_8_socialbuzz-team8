using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Entity;

namespace ProductsAndServices.Context
{
    public class ProductsAndServicesContext : DbContext
    {
        public ProductsAndServicesContext(DbContextOptions<ProductsAndServicesContext> options): base(options)
        {
        }

        public DbSet<ProductService> ProductService { get; set; }
    }
}
