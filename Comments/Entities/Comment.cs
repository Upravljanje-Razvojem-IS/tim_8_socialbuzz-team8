using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Entities
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

        [Required, StringLength(100), MinLength(10)]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }

        public IList<CommentReply> Replies{ get; set; }
    }
}
