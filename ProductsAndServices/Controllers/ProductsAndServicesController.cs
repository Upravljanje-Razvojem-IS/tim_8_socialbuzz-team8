using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Context;
using ProductsAndServices.Entity;
using ProductsAndServices.Entity.DTO;
using System.Collections.Generic;
using System.IO;
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
        [Route("{id}/pictures")]
        public IActionResult GetPicturesForProductServiceById(int id)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            return new JsonResult(productService.Pictures);
        }

        [HttpGet]
        [Route("{id}/pictures/{pictureId}")]
        public IActionResult GetPictureForProductServiceById(int id, int pictureId)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            if (!productService.Pictures.Any(psp => psp.PictureID == pictureId))
            {
                return NotFound();
            }

            var productServicePicture = _context.ProductServicePictures.Find(pictureId);

            return new FileStreamResult(new MemoryStream(productServicePicture.Picture), productServicePicture.ContentType);
        }

        [HttpPost]
        [Route("{id}/pictures")]
        public IActionResult CreatePictureForProductServiceById(int id, [FromForm] IFormFile picture)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            var memoryStream = new MemoryStream();
            picture.CopyTo(memoryStream);

            var newProductServicePicture = new ProductServicePicture()
            {
                Picture = memoryStream.ToArray(),
                FileName = picture.FileName,
                ContentType = picture.ContentType,
                ProductService = productService
            };

            _context.ProductServicePictures.Add(newProductServicePicture);
            _context.SaveChanges();

            return new JsonResult(newProductServicePicture);
        }

        [HttpDelete]
        [Route("{id}/pictures/{pictureId}")]
        public IActionResult DeletePictureFromProductServiceById(int id, int pictureId)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return NotFound();
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            if (!productService.Pictures.Any(psp => psp.PictureID == pictureId))
            {
                return NotFound();
            }

            var deletedProductServicePicture = _context.ProductServicePictures.Find(pictureId);
            _context.ProductServicePictures.Remove(deletedProductServicePicture);
            _context.SaveChanges();

            return new JsonResult(deletedProductServicePicture);
        }
    }
}
