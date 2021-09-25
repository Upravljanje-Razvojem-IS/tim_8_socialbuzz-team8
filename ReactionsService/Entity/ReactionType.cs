using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Entity
{
    public class ReactionType
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }

        public IList<Reaction> ListReactions { get; set; }

    }
}
