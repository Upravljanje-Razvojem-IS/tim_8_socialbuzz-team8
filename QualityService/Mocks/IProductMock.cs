using QualityService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityService.Mocks
{
    public interface IProductMock
    {
        ProductDTO GetProductById(int Id);
    }
}
