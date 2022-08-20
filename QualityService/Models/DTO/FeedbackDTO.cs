using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityService.Models.DTO
{
    public class FeedbackDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public bool IsSelling { get; set; }

        public bool IsPositive { get; set; }

        public string Comment { get; set; }

        public string Response { get; set; }
    }
}
