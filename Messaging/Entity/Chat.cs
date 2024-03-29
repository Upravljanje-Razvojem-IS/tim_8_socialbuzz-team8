﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Messaging.Entity
{
    public class Chat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [JsonIgnore]
        public IList<ChatUser> ChatUsers { get; set; }

        [JsonIgnore]
        public IList<ChatMessage> ChatMessages { get; set; }
    }
}
