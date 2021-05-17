using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsAndServices.Entity
{
    public class ProductServicePrice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceID { get; set; }
        [Required, Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [Required]
        public ProductService ProductService { get; set; }
    }
}
