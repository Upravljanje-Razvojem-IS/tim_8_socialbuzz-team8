using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Entities.DTO
{
    public class CommentDto
    {
        /// <summary>
        /// Comment text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ID of post which comment refers to
        /// </summary>
        public int PostId { get; set; }
    }
}
