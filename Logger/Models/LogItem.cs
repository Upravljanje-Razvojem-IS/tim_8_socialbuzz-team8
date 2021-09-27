using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logger.Entity
{
    public class LogItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoggerID { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required, StringLength(200)]
        public string Message { get; set; }
    }
}