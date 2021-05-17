using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsAndServices.Entity
{
    [Table("ProductService")]
    public class ProductService
    {
        [Key]
        public int ProductServiceID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool PriceAgreement { get; set; }
        public bool IsPriceChangeable { get; set; }
        public bool Exchangement { get; set; }
        public string ExchangementCondition { get; set; }
    }
}
