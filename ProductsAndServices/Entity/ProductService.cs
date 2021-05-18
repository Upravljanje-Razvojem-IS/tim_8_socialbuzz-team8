using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductsAndServices.Entity
{
    public class ProductService
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductServiceID { get; set; }
        [Required]
        public int CreatedByUserID { get; set; }
        [Required, StringLength(50), MinLength(10)]
        public string Title { get; set; }
        [Required, StringLength(1000), MinLength(10)]
        public string Text { get; set; }
        [Required]
        public bool PriceAgreement { get; set; }
        [Required]
        public bool IsPriceChangeable { get; set; }
        [Required]
        public bool Exchangement { get; set; }
        [StringLength(100), MinLength(10)]
        public string ExchangementCondition { get; set; }
        public IList<ProductServicePicture> Pictures { get; set; }
        public IList<ProductServicePrice> Prices { get; set; }
    }
}
