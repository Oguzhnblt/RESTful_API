using RESTful_API.DTO.Entities;

namespace RESTful_API.DTO.Models
{
    public class BookDTO : BaseEntity
    {
        public string BookName { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
