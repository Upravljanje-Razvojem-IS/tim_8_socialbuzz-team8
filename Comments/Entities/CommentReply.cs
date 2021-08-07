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
        /// <summary>
        /// ID of comment reply
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Comment to which is replied to
        /// </summary>
        [Required, JsonIgnore]
        public Comment Comment { get; set; }

        /// <summary>
        /// ID of User who wrote the reply
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Comment reply text
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
