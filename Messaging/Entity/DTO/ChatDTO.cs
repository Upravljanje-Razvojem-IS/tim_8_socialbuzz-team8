using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Entity
{
    public class ChatDTO
    {
        public string Title { get; set; }

        public int? PostId { get; set; }

        public int? ProductServiceId { get; set; }
    }
}
