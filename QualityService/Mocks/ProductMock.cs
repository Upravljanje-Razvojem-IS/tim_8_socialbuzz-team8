using QualityService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityService.Mocks
{
    public class ProductMock : IProductMock
    {
        public static List<ProductDTO> Products { get; set; } = new List<ProductDTO>();

        public ProductMock()
        {
            FillData();
        }

        private static void FillData()
        {
            Products.AddRange(new List<ProductDTO>
            {
                new ProductDTO
                {
                    ProductId = 1,
                    Name = "iPhone 13"
                },
                new ProductDTO
                {
                    ProductId = 2,
                    Name = "Galaxy S22"
                },
                new ProductDTO
                {
                    ProductId = 3,
                    Name = "Zastava 10"
                }
            });
        }
        public ProductDTO GetProductById(int Id)
        {
            return Products.FirstOrDefault(e => e.ProductId == Id);
        }
    }
}
