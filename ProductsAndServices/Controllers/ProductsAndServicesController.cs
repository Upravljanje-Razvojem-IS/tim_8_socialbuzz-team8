using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Context;
using ProductsAndServices.Entity;
using System.Collections.Generic;
using System.Net.Http;
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
            return this._context.ProductServices.ToListAsync();
        }

        [HttpGet]
        [Route("new")]
        public HttpResponseMessage New()
        {
            var psPrice = new ProductServicePrice();
            psPrice.Price = 63;

  

            var ps = new ProductService();
            ps.Title = "naziv";
            ps.Text = "teklst";
            ps.PriceAgreement = false;
            ps.IsPriceChangeable = true;
            ps.Exchangement = true;
            ps.ExchangementCondition = "nesto";

            _context.Add(ps);
            _context.SaveChanges();

            return new HttpResponseMessage();
        }

        [HttpGet]
        [Route("price")]
        public ProductServicePrice Price()
        {
            //var price = _context.ProductServicePrices.Find(1);
            //return price.ProductService;
            return _context.ProductServicePrices.Find(1);
        }

        [HttpGet]
        [Route("prices")]
        public Task<List<ProductServicePrice>> Prices()
        {
            //var price = _context.ProductServicePrices.Find(1);
            //return price.ProductService;
            return _context.ProductServicePrices.Include(p => p.ProductService).ToListAsync();
        }

    }
}
