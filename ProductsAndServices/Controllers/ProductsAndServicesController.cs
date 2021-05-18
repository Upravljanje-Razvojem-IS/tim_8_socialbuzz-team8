using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Context;
using ProductsAndServices.Entity;
using ProductsAndServices.Entity.DTO;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetAll()
        {
            return new JsonResult(_context.ProductServices.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var productService = _context.ProductServices.Find(id);
            
            if (productService == null)
            {
                return NotFound();
            }

            return new JsonResult(productService);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            var deletedProductService = _context.ProductServices.Find(id);

            if (deletedProductService == null)
            {
                return NotFound();
            }

            _context.ProductServices.Remove(deletedProductService);
            _context.SaveChanges();

            return new JsonResult(deletedProductService);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] ProductService productService)
        {
            _context.ProductServices.Add(productService);
            _context.SaveChanges();

            return new JsonResult(productService);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] ProductService productService)
        {
            var currentProductService = _context.ProductServices.Find(id);

            if (currentProductService == null)
            {
                return NotFound();
            }

            currentProductService.Title = productService.Title;
            currentProductService.Text = productService.Text;
            currentProductService.PriceAgreement = productService.PriceAgreement;
            currentProductService.IsPriceChangeable = productService.IsPriceChangeable;
            currentProductService.Exchangement = productService.Exchangement;
            currentProductService.ExchangementCondition = productService.ExchangementCondition;

            _context.ProductServices.Update(currentProductService);
            _context.SaveChanges();

            return new JsonResult(currentProductService);
        }

        [HttpGet]
        [Route("{id}/prices")]
        public IActionResult GetPricesForProductServiceById(int id)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            _context.Entry(productService).Collection(ps => ps.Prices).Load();

            return new JsonResult(productService.Prices);
        }

        [HttpPost]
        [Route("{id}/prices")]
        public IActionResult CreatePriceForProductServiceById(int id, [FromBody] ProductServicePriceDTO productServicePrice)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            var newProductServicePrice = new ProductServicePrice()
            {
                Price = productServicePrice.Price,
                ProductService = productService
            };

            _context.ProductServicePrices.Add(newProductServicePrice);
            _context.SaveChanges();

            return new JsonResult(newProductServicePrice);
        }

        [HttpDelete]
        [Route("{id}/prices/{priceId}")]
        public IActionResult DeletePriceFromProductServiceById(int id, int priceId)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            _context.Entry(productService).Collection(ps => ps.Prices).Load();

            if (!productService.Prices.Any(psp => psp.PriceID == priceId))
            {
                return NotFound();
            }

            var deletedProductServicePrice = _context.ProductServicePrices.Find(priceId);
            _context.ProductServicePrices.Remove(deletedProductServicePrice);
            _context.SaveChanges();

            return new JsonResult(deletedProductServicePrice);
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
