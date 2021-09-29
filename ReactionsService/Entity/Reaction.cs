using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReactionsService.Entity
{
    public class Reaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReactionId { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [ForeignKey("ReactionType")]
        public int ReactionTypeID { get; set; }

        [Required, JsonIgnore]
        public ReactionType ReactionType { get; set; }

    }
}
