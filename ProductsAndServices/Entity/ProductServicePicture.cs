using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductsAndServices.Entity
{
    public class ProductServicePicture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureID { get; set; }
        [Required]
        public byte[] Picture { get; set; }
        [Required, JsonIgnore]
        public ProductService ProductService { get; set; }
    }
}
