using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Messaging.Entity
{
    public class ChatMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Userid { get; set; }

        [Required, JsonIgnore]
        public Chat Chat { get; set; }

        [Required, MinLength(10), MaxLength(1000)]
        public string Message { get; set; }

        public IList<ChatMessageSeen> ChatMessageSeens { get; set; }
    }
}
