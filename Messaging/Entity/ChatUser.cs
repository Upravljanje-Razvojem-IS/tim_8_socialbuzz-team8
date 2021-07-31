using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Entity
{
    public class ChatUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Chat Chat { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool RequestPending { get; set; }
    }
}
