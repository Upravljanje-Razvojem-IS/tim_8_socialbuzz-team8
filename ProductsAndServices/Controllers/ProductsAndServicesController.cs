using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Context;
using ProductsAndServices.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsAndServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAndServicesController : ControllerBase
    {

        private ProductsAndServicesContext _context;

        public ProductsAndServicesController(ProductsAndServicesContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("")]
        public Task<List<ProductService>> GetAll()
        {
            return this._context.ProductService.ToListAsync();
        }

    }
}
