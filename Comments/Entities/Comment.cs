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
        /// <summary>
        /// Comment ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ID of User who created the comment
        /// </summary>
        [Required]
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Comment text
        /// </summary>
        [Required, StringLength(100), MinLength(10)]
        public string Text { get; set; }


        /// <summary>
        /// ID of post which comment refers to
        /// </summary>
        [Required]
        public int PostId { get; set; }


        /// <summary>
        /// List of comment replies
        /// </summary>
        public IList<CommentReply> Replies{ get; set; }
    }
}
