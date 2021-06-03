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
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsAndServicesController : ControllerBase
    {

        private readonly ProductsAndServicesContext _context;

        public ProductsAndServicesController(ProductsAndServicesContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Returns list of all Products/Services
        /// </summary>
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

        /// <summary>
        /// Returns specific Product/Service by its ID
        /// </summary>
        /// <param name="id">ID of wanted Product/Service</param>
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

        /// <summary>
        /// Deletes specific Product/Service by its ID
        /// </summary>
        /// <param name="id">ID of product that will be deleted</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteById(int id, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var deletedProductService = _context.ProductServices.Find(id);

            if (deletedProductService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (deletedProductService.CreatedByUserID != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _context.ProductServices.Remove(deletedProductService);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(deletedProductService));
        }

        /// <summary>
        /// Creates new Product/Service
        /// </summary>
        /// <param name="productService">Product/Service object that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ProductServiceDto productService, [FromHeader] int UserID)
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

        /// <summary>
        /// Updates specific Product/Service by its ID
        /// </summary>
        /// <param name="id">ID of Product/Service that will be updated</param>
        /// <param name="productService">Updated Product/Service that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Update(int id, [FromBody] ProductServiceDto productService, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var currentProductService = _context.ProductServices.Find(id);

            if (currentProductService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (currentProductService.CreatedByUserID != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
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

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(currentProductService));
        }

        /// <summary>
        /// Returns all prices for specific Product/Service by its ID
        /// </summary>
        /// <param name="id">ID of Product/Service whose prices will be listed</param>
        [HttpGet]
        [Route("{id}/Prices")]
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

        /// <summary>
        /// Creates new price for Product/Service targeted by its ID
        /// </summary>
        /// <param name="id">ID of Product/Service</param>
        /// <param name="productServicePrice">Price that will be linked to specific Product/Service</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("{id}/Prices")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult CreatePriceForProductServiceById(int id, [FromBody] ProductServicePriceDto productServicePrice, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (productService.CreatedByUserID != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
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

            return StatusCode(StatusCodes.Status201Created, new JsonResult(newProductServicePrice));
        }


        /// <summary>
        /// Deletes specific price from specific Product/Service
        /// </summary>
        /// <param name="id">ID of Product/Service</param>
        /// <param name="priceId">ID of specific price which will be deleted</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpDelete]
        [Route("{id}/Prices/{priceId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeletePriceFromProductServiceById(int id, int priceId, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (productService.CreatedByUserID != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
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

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(deletedProductServicePrice));
        }

        /// <summary>
        /// Returns all pictures of specific Product/Service
        /// </summary>
        /// <param name="id">ID of Product/Service</param>
        [HttpGet]
        [Route("{id}/Pictures")]
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

        /// <summary>
        /// Returns specific picture of specific Product/Service
        /// </summary>
        /// <param name="id">ID of Product/Service</param>
        /// <param name="pictureId">ID of specific picture</param>
        [HttpGet]
        [Route("{id}/Pictures/{pictureId}")]
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

            var productServicePicture = productService.Pictures.FirstOrDefault(psp => psp.PictureID == pictureId);

            if (productServicePicture == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return new FileStreamResult(new MemoryStream(productServicePicture.Picture), productServicePicture.ContentType);
        }

        /// <summary>
        /// Uploads new picture for specific Product/Service
        /// </summary>
        /// <param name="id">ID of Product/Service</param>
        /// <param name="picture">Picture that will be uploaded and linked to Product/Service</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("{id}/Pictures")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult CreatePictureForProductServiceById(int id, [FromForm] IFormFile picture, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (productService.CreatedByUserID != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
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

            return StatusCode(StatusCodes.Status201Created, new JsonResult(newProductServicePicture));
        }

        /// <summary>
        /// Deletes specific picture from specific Product/Service
        /// </summary>
        /// <param name="id">ID of Product/Service</param>
        /// <param name="pictureId">ID of specific picture that will be deleted</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpDelete]
        [Route("{id}/Pictures/{pictureId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeletePictureFromProductServiceById(int id, int pictureId, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var productService = _context.ProductServices.Find(id);

            if (productService == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (productService.CreatedByUserID != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _context.Entry(productService).Collection(ps => ps.Pictures).Load();

            var deletedProductServicePicture = productService.Pictures.FirstOrDefault(psp => psp.PictureID == pictureId);

            if (deletedProductServicePicture == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.ProductServicePictures.Remove(deletedProductServicePicture);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(deletedProductServicePicture));
        }
    }
}
