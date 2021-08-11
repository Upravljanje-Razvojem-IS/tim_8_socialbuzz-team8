using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models
{
    /// <summary>
    /// Entity class which models cities 
    /// </summary>
    public class City
    {
        /// <summary>
        /// Unique identifier of a city
        /// </summary>
        [Key]
        [Required]
        public Guid CityId { get; set; }

        /// <summary>
        /// City name 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string CityName { get; set; }

        /// <summary>
        /// Indicator whether the City is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Country where city is located
        /// </summary>
        [ForeignKey("CountryId")]
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
