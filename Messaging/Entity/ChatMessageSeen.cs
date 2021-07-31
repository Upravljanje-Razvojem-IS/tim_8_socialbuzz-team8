using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Entity
{
    public class ChatMessageSeen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ChatMessage ChatMessage { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
