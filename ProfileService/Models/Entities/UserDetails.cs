using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models
{
    /// <summary>
    /// Class which models user profile info
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        [Key]
        [Required]
        public Guid UserDetailsID { get; set; }

        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Users first name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Users last name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Users adress
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Adress { get; set; }

        /// <summary>
        /// Users Date of Birth
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User profile description
        /// </summary>
        /// 
        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        /// <summary>
        /// User profile picture
        /// </summary>
        [Required]
        public byte[] ProfilePicture { get; set; }
        /// <summary>
        /// Indicator whether the user account is deleted
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// User's city 
        /// </summary>
        [ForeignKey("CityId")]
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
    }
    
}
