namespace RESTful_API.DTO.Entities
{
    public class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public bool IsActive { get; set; }
        public List<Book> Books { get; set; }


    }
}