using RESTful_API.DTO.Entities;

namespace RESTful_API.DTO.Models
{
    public class GenreDTO : BaseEntity
    {
        public string GenreName { get; set; }
        public bool IsActive { get; set; }

    }
}
