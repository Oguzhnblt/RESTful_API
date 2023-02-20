using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTful_API.DTO.Models
{
    public class UserDTO
    {
        [Required]
        [Column]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
