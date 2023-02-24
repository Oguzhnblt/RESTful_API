namespace RESTful_API.DTO.Models
{
    public class BookDTO
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
