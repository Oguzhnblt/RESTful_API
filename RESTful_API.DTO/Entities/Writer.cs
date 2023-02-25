namespace RESTful_API.DTO.Entities
{
    public class Writer : BaseEntity
    {
        public string WrieterFirstName { get; set; }
        public string WriterLastName { get; set; }
        public DateTime WriterBirthDate { get; set; }

        public List<Book> Books { get; set; }
    }
}
