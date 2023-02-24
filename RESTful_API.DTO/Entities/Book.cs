namespace RESTful_API.DTO.Entities
{
    public class Book : BaseEntity
    {
        public string BookName { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }


}



