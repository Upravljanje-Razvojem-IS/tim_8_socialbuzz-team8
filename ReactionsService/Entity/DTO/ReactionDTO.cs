﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Entity.DTO
{
    public class ReactionDTO
    {
        public int ReactionTypeId { get; set; }
        public int PostId { get; }
        public int CreatedByUserId { get;}
    }
}
