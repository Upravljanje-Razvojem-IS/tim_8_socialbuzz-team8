using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductsAndServices.Entity
{
    public class ProductServicePrice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceID { get; set; }
        [Required, Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [Required, JsonIgnore]
        public ProductService ProductService { get; set; }
    }
}
