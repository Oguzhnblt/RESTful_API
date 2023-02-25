namespace RESTful_API.DTO.Entities
{
    public class Book : BaseEntity
    {
        public string BookName { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }



        public int WriterID { get; set; }
        public Writer Writer { get; set; } = null!;
        public int GenreID { get; set; }
        public Genre Genres { get; set; } = null!;

    }


}



