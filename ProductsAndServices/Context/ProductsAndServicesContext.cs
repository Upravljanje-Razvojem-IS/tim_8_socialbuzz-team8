using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Entity;

namespace ProductsAndServices.Context
{
    public class ProductsAndServicesContext : DbContext
    {
        public ProductsAndServicesContext(DbContextOptions<ProductsAndServicesContext> options): base(options)
        {
        }

        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<ProductServicePicture> ProductServicePictures { get; set; }
        public DbSet<ProductServicePrice> ProductServicePrices { get; set; }
    }
}
