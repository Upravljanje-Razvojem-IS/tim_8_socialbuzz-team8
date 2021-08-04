using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models
{
    //Entity which models a country
    public class Country
    {
        /// <summary>
        /// Unique identifier of a Country
        /// </summary>
        [Key]
        [Required]
        public Guid CountryId { get; set; }
        /// <summary>
        /// Country name 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string CountryName { get; set; }

        public bool Deleted { get; set; }
    }
}
