using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        [Required, JsonIgnore]
        public ReactionType ReactionType { get; set; }

    }
}
