using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndServices.Context;
using ProductsAndServices.Entity;
using ProductsAndServices.Entity.DTO;
using System.IO;
using System.Linq;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(_context.ProductServices.Include(ps => ps.Prices).Include(ps => ps.Pictures).ToList())
            );
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var productService = _context.ProductServices.Find(id);
            
            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(productService));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteById(int id)
        {
            var deletedProductService = _context.ProductServices.Find(id);

            if (deletedProductService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.ProductServices.Remove(deletedProductService);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(deletedProductService));
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ProductServiceDTO productService, [FromHeader] int UserID, [FromHeader] string UserName)
        {
            var newProductService = new ProductService()
            {
                CreatedByUserID = UserID,
                Title = productService.Title,
                Text = productService.Text,
                PriceAgreement = productService.PriceAgreement,
                IsPriceChangeable = productService.IsPriceChangeable,
                Exchangement = productService.Exchangement,
                ExchangementCondition = productService.ExchangementCondition
            };

            _context.ProductServices.Add(newProductService);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(newProductService));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, [FromBody] ProductServiceDTO productService)
        {
            var currentProductService = _context.ProductServices.Find(id);

            if (currentProductService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            currentProductService.Title = productService.Title;
            currentProductService.Text = productService.Text;
            currentProductService.PriceAgreement = productService.PriceAgreement;
            currentProductService.IsPriceChangeable = productService.IsPriceChangeable;
            currentProductService.Exchangement = productService.Exchangement;
            currentProductService.ExchangementCondition = productService.ExchangementCondition;

            _context.ProductServices.Update(currentProductService);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(currentProductService));
        }

        [HttpGet]
        [Route("{id}/prices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPricesForProductServiceById(int id)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(productService).Collection(ps => ps.Prices).Load();

            return StatusCode(StatusCodes.Status200OK, new JsonResult(productService.Prices));
        }

        [HttpPost]
        [Route("{id}/prices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePriceForProductServiceById(int id, [FromBody] ProductServicePriceDTO productServicePrice)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var newProductServicePrice = new ProductServicePrice()
            {
                Price = productServicePrice.Price,
                ProductService = productService
            };

            _context.ProductServicePrices.Add(newProductServicePrice);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(newProductServicePrice));
        }

        [HttpDelete]
        [Route("{id}/prices/{priceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePriceFromProductServiceById(int id, int priceId)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(productService).Collection(ps => ps.Prices).Load();

            if (!productService.Prices.Any(psp => psp.PriceID == priceId))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var deletedProductServicePrice = _context.ProductServicePrices.Find(priceId);
            _context.ProductServicePrices.Remove(deletedProductServicePrice);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(deletedProductServicePrice));
        }

        [HttpGet]
        [Route("{id}/pictures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPicturesForProductServiceById(int id)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            return StatusCode(StatusCodes.Status200OK, new JsonResult(productService.Pictures));
        }

        [HttpGet]
        [Route("{id}/pictures/{pictureId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPictureForProductServiceById(int id, int pictureId)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            if (!productService.Pictures.Any(psp => psp.PictureID == pictureId))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var productServicePicture = productService.Pictures.Where(psp => psp.PictureID == pictureId).FirstOrDefault();

            return new FileStreamResult(new MemoryStream(productServicePicture.Picture), productServicePicture.ContentType);
        }

        [HttpPost]
        [Route("{id}/pictures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePictureForProductServiceById(int id, [FromForm] IFormFile picture)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(newProductServicePicture));
        }

        [HttpDelete]
        [Route("{id}/pictures/{pictureId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePictureFromProductServiceById(int id, int pictureId)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            if (!productService.Pictures.Any(psp => psp.PictureID == pictureId))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var deletedProductServicePicture = productService.Pictures.Where(psp => psp.PictureID == pictureId).FirstOrDefault();
            _context.ProductServicePictures.Remove(deletedProductServicePicture);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(deletedProductServicePicture));
        }
    }
}
