using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models
{
    /// <summary>
    /// Entity which models profile details of a 
    /// corporate user
    /// </summary>
    public class CorporateUserDetails : UserDetails
    {
        /// <summary>
        /// Identification number of a company given by a state 
        /// </summary>
        [Required]
        [StringLength (8)]
        public string Pib { get; set; }
        /// <summary>
        /// Corporation name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CorporationName { get; set; }
        /// <summary>
        /// The date when corporation was founded
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
