using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Entities.DTO
{
    public class CommentDto
    {
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
