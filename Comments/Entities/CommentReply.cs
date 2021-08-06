using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Comments.Entities
{
    public class CommentReply
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, JsonIgnore]
        public Comment Comment { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
