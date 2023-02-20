using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTful_API.DTO.Models
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
