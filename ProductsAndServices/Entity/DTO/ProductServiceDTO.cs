using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServices.Entity.DTO
{
    public class ProductServiceDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool PriceAgreement { get; set; }
        public bool IsPriceChangeable { get; set; }
        public bool Exchangement { get; set; }
        public string ExchangementCondition { get; set; }
    }
}
